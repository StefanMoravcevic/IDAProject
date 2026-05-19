using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using IDAProject.Web.Api.Models.Common;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Api.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Companies;
using IDAProject.Web.Models.RequestModels.Employees;
using IDAProject.Web.Models.RequestModels.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IDAProject.Web.Api.Managers
{
    public class NotificationsManager : INotificationsManager
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly ITasksPlanningCommentsRepository _tasksPlanningCommentsRepository;
        private readonly ITasksRealizationCommentsRepository _tasksRealizationCommentsRepository;
        private readonly IEmailJobSettingsRepository _emailJobSettingsRepository;
        private readonly IMasterDataRepository _masterDataRepository;
        private readonly IQueueRepository _queueRepository;
        private readonly IDocumentsManager _documentsRepository;
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger _logger;
        private readonly EmailQueueSettings _emailQueueSettings;


        public NotificationsManager(
            INotificationsRepository notificationsRepository,
            ILogger<NotificationsManager> logger,
            IOptions<EmailQueueSettings> options,
            IQueueRepository queueRepository,
            ITasksPlanningCommentsRepository tasksPlanningCommentsRepository,
            ITasksRealizationCommentsRepository tasksRealizationCommentsRepository,
            IDocumentsManager documentsRepository,
            IMasterDataRepository masterDataRepository,
            ICompaniesRepository companiesRepository,
            IEmployeesRepository employeesRepository, 
            IUsersRepository usersRepository)
        {
            _logger = logger;
            _notificationsRepository = notificationsRepository;
            _queueRepository = queueRepository;
            _emailQueueSettings = options.Value;
            _documentsRepository = documentsRepository;
            _masterDataRepository = masterDataRepository;
            _companiesRepository = companiesRepository;
            _employeesRepository = employeesRepository;
            _usersRepository = usersRepository;
            _tasksPlanningCommentsRepository = tasksPlanningCommentsRepository;
            _tasksRealizationCommentsRepository = tasksRealizationCommentsRepository;
        }


        public async Task SendQueuedEmailsAsync()
        {
            FileStream? fileStream = null;
            try
            {
                var items = await _queueRepository.GetNextEmailQueueAsync(_emailQueueSettings.SinglePassSize);

                foreach (var item in items)
                {
                    // very imporant:
                    // we have 2 try/catch blocks: outer and inner
                    // - outer used for request exception handling e.g: when GetNextEmailQueueAsync
                    // - inner used per single item error handling and to prevent blocking of all pending email items to be sent
                    try
                    {
                        var attachedDocuments = await _documentsRepository.GetDocumentsByReferenceIdAsync(DocumentTypeConstants.Email_Queue_Attachment, item.Id);

                        var smtpClient = GetDefaultSmtpClient();

                        var mailMessage = new MailMessage(_emailQueueSettings.From, item.EmailTo);
                        mailMessage.Subject = item.Subject;
                        mailMessage.Body = item.Body;
                        mailMessage.IsBodyHtml = item.IsBodyHtml;

                        foreach (var attachedDocument in attachedDocuments.Payload)
                        {
                            var documentDataResponse = await _documentsRepository.GetDocumentDataById(attachedDocument.Id);

                            if (documentDataResponse.Valid)
                            {
                                fileStream = new FileStream(documentDataResponse.Payload!.FullPath, FileMode.Open);
                                var attachment = new Attachment(fileStream, attachedDocument.DownloadFileName, documentDataResponse.Payload!.MimeType);
                                attachment.ContentId = attachedDocument.DownloadFileName;
                                mailMessage.Attachments.Add(attachment);
                            }
                        }

                        await smtpClient.SendMailAsync(mailMessage);

                        await _queueRepository.UpdateEmailQueueAsSentAsync(item.Id);
                    }
                    catch (Exception singleItemException)
                    {
                        if (fileStream != null)
                        {
                            fileStream.Dispose();
                        }
                        var itemJson = JsonConvert.SerializeObject(item);
                        _logger.LogError(singleItemException, itemJson);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"singlePassSize: {_emailQueueSettings.SinglePassSize}");
            }
        }

        private SmtpClient GetDefaultSmtpClient()
        {
            var smtpClient = new SmtpClient
            {
                Host = _emailQueueSettings.Host,
                Port = _emailQueueSettings.Port,
                EnableSsl = _emailQueueSettings.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailQueueSettings.Username, _emailQueueSettings.Password)
            };
            return smtpClient;
        }

        public async Task<string> GetGenericEmailTemplateAsync(int companyId, string title)
        {
            var company = await _companiesRepository.GetCompanyAsync(companyId);

            var emailTemplate = GetEmbededContentString("GenericEmailTemplate.htm");
            emailTemplate = emailTemplate.Replace("##title##", title);
            emailTemplate = emailTemplate.Replace("##company##", company.Name);
            return emailTemplate;
        }

        #region Private methods

        private static string GetEmailReceivers(IEnumerable<EmployeeDto> employees, params int[] jobTypes)
        {
            var result = string.Empty;

            foreach (var emp in employees)
            {
                if (DataHelpers.IsValidEmail(emp.Email) && (jobTypes.Length == 0 ))
                {
                    if (result == string.Empty)
                    {
                        result = emp.Email;
                    }
                    else
                    {
                        result += $"; {emp.Email}";
                    }
                }
            }
            return result;
        }


        private static string GetEmbededContentString(string contentName)
        {
            var managerAssembly = Assembly.GetExecutingAssembly();
            var resourceName = $"IDAProject.Web.Api.Managers.Embeded.{contentName}";

            using (var stream = managerAssembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream!))
            {
                return reader.ReadToEnd();
            }
        }

        #endregion
        
        public async Task<ResponseModelList<EmailDto>> SearchEmailsAsync(SearchEmailsParams searchParams)
        {
            var result = new ResponseModelList<EmailDto>();
            try
            {
                var employee = await _employeesRepository.SearchEmployeesAsync(new SearchEmployeesParams() { Id = searchParams.DispatcherId });
                searchParams.DispatcherEmail = employee.FirstOrDefault()!.Email;
                result.Payload = await _queueRepository.SearchEmailsAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModelBase> SendNewAccountRequest(RegisterModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                var externalJobSettings = await _emailJobSettingsRepository.GetEmailJobSettingsAsync(EmailJobTypes.ResetPassword);
                if (!externalJobSettings.Any())
                {
                    _logger.LogError("External job settings for ResetPassword job type are not correct.");
                    result.Valid = false;
                    return result;
                }

                var enabledExternalJobSettings = externalJobSettings.Where(x => x.Enabled == true);
                if (!enabledExternalJobSettings.Any())
                {
                    _logger.LogInformation("Job type: ResetPassword is disabled.");
                    result.Valid = false;
                    return result;
                }
                var rowItemTemplate = GetEmbededContentString("TableRowTemplate.htm");
                var templateBody = await GetGenericEmailTemplateAsync(1, "Zahtev za registraciju naloga");

                var rows = new StringBuilder();
                rows.Append(rowItemTemplate.Replace("##key##", "Date:").Replace("##value##", Convert.ToString(DateTime.Today.Date.ToShortDateString())));
                rows.Append(rowItemTemplate.Replace("##key##", "User name:").Replace("##value##", Convert.ToString(requestModel.Username!)));
                rows.Append(rowItemTemplate.Replace("##key##", "E-mail:").Replace("##value##", Convert.ToString(requestModel.Email!)));

                templateBody = templateBody.Replace("##content##", rows.ToString());
                var usersWithRole = await _usersRepository.GetUsersByRoleIdAsync((int)AspNetRoles.Administrator);
                string emailString = string.Join(", ", usersWithRole.Select(user => user.Email));

                await _queueRepository.AddEmailQueueAsync("Zahtev za registraciju naloga", externalJobSettings.FirstOrDefault().EmailString, string.Empty, templateBody, true);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {JsonConvert.SerializeObject(requestModel)}");
            }
            return result;
        }

        public async Task<ResponseModelBase> SendResetPasswordRequest(RegisterModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                var externalJobSettings = await _emailJobSettingsRepository.GetEmailJobSettingsAsync(EmailJobTypes.ResetPassword);
                if (!externalJobSettings.Any())
                {
                    _logger.LogError("External job settings for ResetPassword job type are not correct.");
                    result.Valid = false;
                    return result;
                }

                var enabledExternalJobSettings = externalJobSettings.Where(x => x.Enabled == true);
                if (!enabledExternalJobSettings.Any())
                {
                    _logger.LogInformation("Job type: ResetPassword is disabled.");
                    result.Valid = false;
                    return result;
                }
                var rowItemTemplate = GetEmbededContentString("TableRowTemplate.htm");
                var templateBody = await GetGenericEmailTemplateAsync(1, "Zahtev za resetovanje korisničke šifre");

                var rows = new StringBuilder();
                rows.Append(rowItemTemplate.Replace("##key##", "Date:").Replace("##value##", Convert.ToString(DateTime.Today.Date.ToShortDateString())));
                rows.Append(rowItemTemplate.Replace("##key##", "User name:").Replace("##value##", Convert.ToString(requestModel.Username!)));
                rows.Append(rowItemTemplate.Replace("##key##", "E-mail:").Replace("##value##", Convert.ToString(requestModel.Email!)));

                templateBody = templateBody.Replace("##content##", rows.ToString());

                await _queueRepository.AddEmailQueueAsync("Zahtev za resetovanje korisničke šifre", externalJobSettings.FirstOrDefault().EmailString, string.Empty, templateBody, true);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {JsonConvert.SerializeObject(requestModel)}");
            }
            return result;
        }

        public async Task<ResponseModelBase> SendAdminResetPassword(string pass, string userName, string mailTo, string textTitle)
        {
            var result = new ResponseModelBase();
            try
            {
                var smtpClient = GetDefaultSmtpClient();

                var rowItemTemplate = GetEmbededContentString("TableRowTemplate.htm");
                var templateBody = await GetGenericEmailTemplateAsync(1, textTitle);

                var rows = new StringBuilder();
                rows.Append(rowItemTemplate.Replace("##key##", "User name:").Replace("##value##", Convert.ToString(userName)));
                rows.Append(rowItemTemplate.Replace("##key##", "Password:").Replace("##value##", Convert.ToString(pass)));

                templateBody = templateBody.Replace("##content##", rows.ToString());
                //kraj

                await _queueRepository.AddEmailQueueAsync(textTitle, mailTo, _emailQueueSettings.From, templateBody, true);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"UserName: {userName}");
            }
            return result;
        }

        public async Task<ResponseModelBase> SendCommentPlanNotification(int? taskPlanCommentId)
        {
            var result = new ResponseModelBase();

            if (!taskPlanCommentId.HasValue)
            {
                result.Valid = false;
                result.Message = "Komentar ID nije prosleđen";
                return result;
            }

            var comment = await _tasksPlanningCommentsRepository
                .GetTasksPlanningCommentByIdAsync(taskPlanCommentId.Value);

            if (comment == null)
            {
                result.Valid = false;
                result.Message = "Komentar nije pronađen";
                return result;
            }

            // -----------------------------------
            // 1. DETEKCIJA DA LI JE REPLY
            // -----------------------------------
            var isReply = comment.ParentTaskPlanningCommentId.HasValue;

            TasksPlanningCommentDto parentComment = null;
            int employeeIdToNotify;

            if (isReply)
            {
                parentComment = await _tasksPlanningCommentsRepository
                    .GetTasksPlanningCommentByIdAsync(comment.ParentTaskPlanningCommentId.Value);

                if (parentComment == null)
                {
                    result.Valid = false;
                    result.Message = "Parent komentar nije pronađen";
                    return result;
                }

                // owner originalnog komentara prima email
                employeeIdToNotify = parentComment.EmployeeForReplyId.Value;
            }
            else
            {
                // owner plana prima email
                employeeIdToNotify = comment.EmployeeId.Value;
            }

            var employee = await _employeesRepository.GetEmployeeByIdAsync(employeeIdToNotify);

            if (employee == null)
            {
                result.Valid = false;
                result.Message = "Zaposleni nije pronađen";
                return result;
            }

            // -----------------------------------
            // 2. SUBJECT
            // -----------------------------------
            var textTitle = isReply
                ? $"Odgovor na vaš komentar za {comment.PlanDateFormattedForComment}"
                : $"Novi komentar na vaš plan za {comment.PlanDateFormattedForComment}";

            var mailTo = employee.Email;

            // -----------------------------------
            // 3. EMAIL BODY
            // -----------------------------------
            string templateBody;

            if (isReply)
            {
                templateBody = $@"
                <p>Poštovani/na,</p>

                <p><strong>{comment.Username}</strong> je odgovorio na vaš komentar za plan dana <strong>{comment.PlanDateFormattedForComment}</strong>.</p>

                <p><strong>Odnosi se na:</strong></p>
                <p>
                <strong>Zadatak:</strong> {comment.DisplayTask}<br/>
                <strong>Aktivnost:</strong> {comment.Activity}
                </p>

                <p><strong>Originalni komentar:</strong></p>
                <div style='padding:10px;background:#f1f3f5;border-left:4px solid #6c757d;border-radius:4px;'>
                    {parentComment.Comment}
                </div>

                <p style='margin-top:10px;'><strong>Odgovor:</strong></p>
                <div style='padding:10px;background:#f8f9fa;border-left:4px solid #0d6efd;border-radius:4px;'>
                    {comment.Comment}
                    <div style='margin-top:5px;font-size:0.85em;color:#6c757d;'>
                        Odgovor dat: {comment.CreatedAtFormatted}
                    </div>
                </div>";
            }
            else
            {
                templateBody = $@"
                <p>Poštovani/na,</p>

                <p>Obaveštavamo vas da je zaposleni <strong>{comment.Username}</strong> uneo komentar na vaš plan za dan <strong>{comment.PlanDateFormattedForComment}</strong>.</p>

                <p><strong>Zadatak:</strong> {comment.DisplayTask}<br/>
                <strong>Opis:</strong> {comment.Activity}</p>

                <p><strong>Tekst komentara:</strong></p>
                <div style='padding:10px;background:#f8f9fa;border-left:4px solid #0d6efd;border-radius:4px;'>
                    {comment.Comment}
                    <div style='margin-top:5px;font-size:0.85em;color:#6c757d;'>
                        Komentar dodat: {comment.CreatedAtFormatted}
                    </div>
                </div>";
            }

            // -----------------------------------
            // 4. QUEUE EMAIL
            // -----------------------------------
            try
            {
                await _queueRepository.AddEmailQueueAsync(
                    textTitle,
                    mailTo,
                    _emailQueueSettings.From,
                    templateBody,
                    true
                );

                result.Valid = true;
                result.Message = isReply
                    ? "Reply email uspešno stavljen u queue."
                    : "Email obaveštenje uspešno stavljeno u queue.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška prilikom dodavanja emaila u queue");

                result.Valid = false;
                result.Message = "Došlo je do greške prilikom dodavanja emaila u queue.";
            }

            return result;
        }

        public async Task<ResponseModelBase> SendCommentRealizationNotification(int? taskRealizationCommentId)
        {
            var result = new ResponseModelBase();

            if (!taskRealizationCommentId.HasValue)
            {
                result.Valid = false;
                result.Message = "Komentar ID nije prosleđen";
                return result;
            }

            var comment = await _tasksRealizationCommentsRepository
                .GetTasksRealizationCommentByIdAsync(taskRealizationCommentId.Value);

            if (comment == null)
            {
                result.Valid = false;
                result.Message = "Komentar nije pronađen";
                return result;
            }

            var isReply = comment.ParentTaskRealizationCommentId.HasValue;

            TasksRealizationCommentDto parentComment = null;
            int employeeIdToNotify;

            // -----------------------------------
            // 1. KO PRIMA EMAIL
            // -----------------------------------
            if (isReply)
            {
                parentComment = await _tasksRealizationCommentsRepository
                    .GetTasksRealizationCommentByIdAsync(comment.ParentTaskRealizationCommentId.Value);

                if (parentComment == null)
                {
                    result.Valid = false;
                    result.Message = "Parent komentar nije pronađen";
                    return result;
                }

                employeeIdToNotify = parentComment.EmployeeForReplyId.Value;
            }
            else
            {
                employeeIdToNotify = comment.EmployeeId.Value;
            }

            var employee = await _employeesRepository.GetEmployeeByIdAsync(employeeIdToNotify);

            if (employee == null)
            {
                result.Valid = false;
                result.Message = "Zaposleni nije pronađen";
                return result;
            }

            // -----------------------------------
            // 2. SUBJECT
            // -----------------------------------
            var textTitle = isReply
                ? $"Odgovor na vaš komentar za realizaciju {comment.RealizationDateFormattedForComment}"
                : $"Novi komentar na vašu realizaciju za {comment.RealizationDateFormattedForComment}";

            var mailTo = employee.Email;

            // -----------------------------------
            // 3. BODY
            // -----------------------------------
            string templateBody;

            if (isReply)
            {
                templateBody = $@"
<p>Poštovani/na,</p>

<p><strong>{comment.Username}</strong> je odgovorio na vaš komentar za realizaciju dana <strong>{comment.RealizationDateFormattedForComment}</strong>.</p>

<p><strong>Odnosi se na:</strong></p>
<p>
<strong>Zadatak:</strong> {comment.DisplayTask}<br/>
<strong>Aktivnost:</strong> {comment.Activity}
</p>

<p><strong>Originalni komentar:</strong></p>
<div style='padding:10px;background:#f1f3f5;border-left:4px solid #6c757d;border-radius:4px;'>
    {parentComment.Comment}
</div>

<p style='margin-top:10px;'><strong>Odgovor:</strong></p>
<div style='padding:10px;background:#f8f9fa;border-left:4px solid #0d6efd;border-radius:4px;'>
    {comment.Comment}
    <div style='margin-top:5px;font-size:0.85em;color:#6c757d;'>
        Odgovor dat: {comment.CreatedAtFormatted}
    </div>
</div>";
            }
            else
            {
                templateBody = $@"
<p>Poštovani/na,</p>

<p>Obaveštavamo vas da je zaposleni <strong>{comment.Username}</strong> uneo komentar na vašu realizaciju za dan <strong>{comment.RealizationDateFormattedForComment}</strong>.</p>

<p><strong>Zadatak:</strong> {comment.DisplayTask}<br/>
<strong>Aktivnost:</strong> {comment.Activity}</p>

<p><strong>Tekst komentara:</strong></p>
<div style='padding:10px;background:#f8f9fa;border-left:4px solid #0d6efd;border-radius:4px;'>
    {comment.Comment}
    <div style='margin-top:5px;font-size:0.85em;color:#6c757d;'>
        Komentar dodat: {comment.CreatedAtFormatted}
    </div>
</div>";
            }

            // -----------------------------------
            // 4. QUEUE
            // -----------------------------------
            try
            {
                await _queueRepository.AddEmailQueueAsync(
                    textTitle,
                    mailTo,
                    _emailQueueSettings.From,
                    templateBody,
                    true
                );

                result.Valid = true;
                result.Message = isReply
                    ? "Reply email uspešno stavljen u queue."
                    : "Email obaveštenje uspešno stavljeno u queue.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška prilikom dodavanja emaila u queue");

                result.Valid = false;
                result.Message = "Došlo je do greške prilikom dodavanja emaila u queue.";
            }

            return result;
        }
        public async Task<ResponseModelBase> SendUserCreatedEmail(string email, string password, string username)
        {
            var result = new ResponseModelBase();

            var body = $@"
            Poštovani/a,

            Kreiran Vam je nalog na aplikaciji IDA.

            Link za pristup aplikaciji je: https://ida.alpackgroup.com/

            Kredencijali za pristup aplikaciji:

            Korisničko ime: {username}
            Lozinka: {password}

            Ukoliko imate bilo kakvih pitanja možete se obratiti na mejl adresu hr@alpackgroup.com.

            Srdačan pozdrav.
            ";

            try
            {
                await _queueRepository.AddEmailQueueAsync(
                    "Kreiranje korisničkog naloga na IDA aplikaciji",
                    email,
                    _emailQueueSettings.From,
                    body,
                    false
                );

                result.Valid = true;
                result.Message = "Email obaveštenje uspešno stavljeno u queue.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška prilikom dodavanja emaila u queue");
                result.Valid = false;
                result.Message = "Došlo je do greške prilikom dodavanja emaila u queue.";
            }

            return result;


        }
    }
}   