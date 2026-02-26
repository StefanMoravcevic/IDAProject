using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Messages;

namespace IDAProject.Web.Api.Managers
{
    public class MessagesManager : IMessagesManager
    {
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IDocumentsManager _documentsManager;
        private readonly ILogger _logger;

        public MessagesManager(
            ILogger<PartnersManager> logger,
            IMessagesRepository messagesRepository,
            IDocumentsManager documentsManager,
            IUsersRepository usersRepository)
        {
            _logger = logger;
            _messagesRepository = messagesRepository;
            _documentsManager = documentsManager;
            _usersRepository = usersRepository;
        }


        public async Task<ResponseModel<int>> SendMessageToUserAsync(SaveUserMessageRequestModel message, MemoryStream? fileContent, string fileName)
        {
            var response = new ResponseModel<int>();

            try
            {
                message.CreatedDate = DateTime.UtcNow;
                response.Payload = await _messagesRepository.InsertMessageAsync(message);

                var senderId = await _messagesRepository.GetFcmTokenAsync(message.UserFrom);
                var recipId = await _messagesRepository.GetFcmTokenAsync(message.UserTo);

                var senderFullName = await _usersRepository.GetUserFullNameAsync(message.UserFrom);

                int referenceDocumentId = 0;

                if (fileContent != null && fileContent.Length > 0)
                {
                    var uploadFileRequestModel = new UploadFileRequestModel
                    {
                        DocumentTypeId = DocumentTypeConstants.Message_image,
                        FileName = fileName,
                        UserId = message.UserFrom,
                        ReferenceId = response.Payload
                    };

                    var fileUploadResponse = await _documentsManager.UploadFileAsync(fileContent, uploadFileRequestModel);

                    if (fileUploadResponse.Valid)
                    {
                        referenceDocumentId = fileUploadResponse.Payload;
                    }
                }

                var newMessage = new Message
                {
                    Token = recipId,
                    Notification = new Notification
                    {
                        Title = senderFullName,
                        Body = message.Message
                    },
                    Data = new Dictionary<string, string>
                    {
                        { "userFrom", message.UserFrom.ToString("F0") },
                        { "id", response.Payload.ToString("F0") },
                        { "referenceDocumentId", referenceDocumentId.ToString("F0") }
                    }
                };

                var messageId = await FirebaseMessaging.DefaultInstance.SendAsync(newMessage);
                response.Valid = true;
            }
            catch (Exception e)
            {
                var requestModelJson = JsonConvert.SerializeObject(message);
                _logger.LogError(e, requestModelJson);
                response.Valid = false;
            }
            return response;
        }


        public async Task<ResponseModelBase> UpdateFcmTokenAsync(int idUser, string token)
        {
            var result = new ResponseModelBase();

            try
            {
                await _messagesRepository.UpdateFcmTokenAsync(idUser, token);
                result.Valid = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"idUser: {idUser}, token: {token}");
            }

            return result;
        }

        public async Task<ResponseModelList<Conversation>> GetUserConversationsAsync(int idUser)
        {
            var result = new ResponseModelList<Conversation>();

            try
            {
                result.Payload = await _messagesRepository.GetUserConversationsAsync(idUser);

                foreach (var convesation in result.Payload)
                {
                    // sender is always user
                    convesation.SenderImage = $"media/user-avatar/{convesation.IdSender}";
                    convesation.ReceiptImage = $"media/user-avatar/{convesation.IdReceipt}";
                }

                result.Payload = result.Payload.
                    OrderByDescending(x => x.LastMessageDate)
                    .ToList();

                result.Valid = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"idUser: {idUser}");
            }

            return result;
        }

        public async Task<ResponseModelList<UserMessageDto>> GetConversationMessagesAsync(int idUser, int idUserFrom)
        {
            var result = new ResponseModelList<UserMessageDto>();

            try
            {
                result.Payload = await _messagesRepository.GetConversationMessagesAsync(idUser, idUserFrom);
                result.Valid = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"idUser: {idUser}, idUserFrom: {idUserFrom}");
            }

            return result;
        }

        public async Task<byte[]> GetUserAvatarAsync(int idUser)
        {
            var initials = await _usersRepository.GetUserInitialsAsync(idUser);

            var w = 128;
            var h = 128;

            using (var bitmap = new Bitmap(w, h))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.Transparent);
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    var p1 = new Point(0, 0);
                    var p2 = new Point(w, h);

                    using (Brush b = new LinearGradientBrush(p1, p2, ColorTranslator.FromHtml("#9F0460"), ColorTranslator.FromHtml("#EE048F")))
                    {
                        g.FillEllipse(b, 0, 0, w - 2, h - 2);
                    }

                    float emSize = 52;

                    var font = new Font("Calibri", emSize, FontStyle.Bold);
                    var brush = new LinearGradientBrush(p1, p2, Color.White, ColorTranslator.FromHtml("#D1D1D1"));

                    var format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;

                    g.DrawString(initials, font, brush, new RectangleF(0, 0, w, h), format);
                }

                using (var memStream = new MemoryStream())
                {
                    bitmap.Save(memStream, ImageFormat.Png);
                    var result = memStream.GetBuffer();
                    return result;
                }
            }
        }

        public async Task<ResponseModelList<ContactInfo>> GetProposedContactsAsync(int idUser)
        {
            var result = new ResponseModelList<ContactInfo>();

            try
            {
                result.Payload = await _messagesRepository.GetProposedContactsAsync(idUser);
                result.Valid = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"idUser: {idUser}");
            }

            return result;
        }

        public async Task<ResponseModelList<ContactInfo>> SearchContactsByKeywordAsync(int companyId, string keyword)
        {
            var result = new ResponseModelList<ContactInfo>();

            try
            {
                result.Payload = await _messagesRepository.SearchContactsByKeywordAsync(companyId, keyword);
                result.Valid = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"keyword: {keyword}");
            }

            return result;
        }

        public async Task<ResponseModelList<UserMessageDto>> SearchUserMessagesAsync(SearchUserMessagesParams searchParams)
        {
            var result = new ResponseModelList<UserMessageDto>();
            try
            {
                result.Payload = await _messagesRepository.SearchUserMessagesAsync(searchParams);
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
    }
}