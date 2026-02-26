using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.RequestModels.Messages;

namespace IDAProject.Web.Api.Repositories
{
    public class QueueRepository : IQueueRepository
    {

        private readonly IDAProjectContext _dbContext;

        public QueueRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddEmailQueueAsync(string subject, string emailTo, string emailCc, string body, bool isBodyHtml)
        {
            var documentRecord = new EmailQueue
            {
                Subject = subject,
                EmailTo = emailTo,
                EmailCc = emailCc,
                Body = body,
                IsBodyHtml = isBodyHtml,
                DateQueued = DateTime.UtcNow,
                DateSent = null,
                IsDeleted = false
            };

            _dbContext.EmailQueues.Add(documentRecord);
            await _dbContext.SaveChangesAsync();
            return documentRecord.Id;
        }

        public async Task<IEnumerable<EmailQueueDto>> GetNextEmailQueueAsync(int maxItemsCount)
        {
            var query = from eq in _dbContext.EmailQueues
                        where eq.DateSent == null
                        orderby eq.DateQueued descending     
                        select DataHelpers.CloneObjectWithIL<EmailQueue, EmailQueueDto>(eq);

            query = query.Take(maxItemsCount);

            return await query.ToListAsync();
        }


        public async Task UpdateEmailQueueAsSentAsync(int id)
        {
            var dbRecord = await _dbContext.EmailQueues.FirstAsync(x => x.Id == id);
            dbRecord.DateSent = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EmailDto>> SearchEmailsAsync(SearchEmailsParams searchParams)
        {

            var result = new List<EmailDto>();
            IQueryable<EmailQueue> query = _dbContext.EmailQueues.Where(x => x.IsDeleted == false);

            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.FromDate.HasValue)
                {
                    if (searchParams.ToDate.HasValue)
                    {
                        query = query.Where(x => x.DateSent >= searchParams.FromDate && x.DateSent < searchParams.ToDate.Value.Date.AddDays(1));
                    }
                    else
                    {
                        query = query.Where(x => x.DateSent!.Value.Date == searchParams.FromDate);
                    }
                }
                else
                {
                    if (searchParams.LastXDays.HasValue)
                    {
                        query = query.Where(x => x.DateSent >= DateTime.Now.AddDays(-searchParams.LastXDays.Value));
                    }
                }
                if (!string.IsNullOrEmpty(searchParams.DispatcherEmail))
                {
                    query = query.Where(x => x.EmailTo.Contains(searchParams.DispatcherEmail) || x.EmailCc!.Contains(searchParams.DispatcherEmail));
                }

                if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    query = query.Where(x => x.Subject.ToUpper().Contains(searchParams.Keyword.ToUpper()) || x.Body.ToUpper().Contains(searchParams.Keyword.ToUpper()));
                }
            }

            result = await (from em in query
                            select new EmailDto
                            {
                                Id = em.Id,
                                Message = $"<a href=\"data:text/html;base64,{Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(em.Body))}\" target=\"_blank\">View e-mail content</a>",
                                //Message = em.Body,
                                Subject = em.Subject,
                                SentDate = em.DateSent,
                                Sender = "OrbitX system",
                                //Receiver = em.UserToNavigation.Employee.Name + " " + em.UserToNavigation.Employee.Surname,
                                ReceiverEmail = em.EmailTo + (!string.IsNullOrEmpty(em.EmailCc) ? "; " + em.EmailCc : ""),
                                //ReceiverUser = em.UserToNavigation.UserName

                            }).ToListAsync();
            foreach (var message in result)
            {
                var files = await (from doc in _dbContext.Documents
                                   where doc.ReferenceId == message.Id && doc.IsDeleted == false && doc.DocumentTypeId == DocumentTypeConstants.Email_Queue_Attachment
                                   select new
                                   {
                                       File = doc.DownloadFileName ?? string.Empty
                                   })
                    .ToListAsync();
                if (files.Any())
                {
                    message.Attachment = files.FirstOrDefault()!.File;
                }
            }

            return result;


        }
    }
}
