using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Messages;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesManager _messagesManager;
        private readonly INotificationsManager _notificationsManager;

        public MessagesController(IMessagesManager messagesManager, INotificationsManager notificationsManager) : base()
        {
            _messagesManager = messagesManager;
            _notificationsManager = notificationsManager;
        }

        [HttpPut("token/{idUser}")]
        public async Task<ResponseModelBase> UpdateFcmTokenAsync(int idUser, UpdateFcmTokenRequestModel model)
        {
            var result = await _messagesManager.UpdateFcmTokenAsync(idUser, model.Token);
            return result;
        }

        [HttpPost("send/user")]
        public async Task<ResponseModelBase> SendMessageToUserAsync(SaveUserMessageRequestModel message)
        {
            var response = await _messagesManager.SendMessageToUserAsync(message, null, string.Empty);
            return response;
        }

        [HttpPost("searchUserMessages")]
        public async Task<ResponseModelList<UserMessageDto>> SearchUserMessagesAsync(SearchUserMessagesParams searchParams)
        {
            var response = await _messagesManager.SearchUserMessagesAsync(searchParams);
            return response;
        }

        [HttpPost("searchEmails")]
        public async Task<ResponseModelList<EmailDto>> SearchEmailsAsync(SearchEmailsParams searchParams)
        {
            var response = await _notificationsManager.SearchEmailsAsync(searchParams);
            return response;
        }

        [HttpPost("send/file-to-user")]
        public async Task<ResponseModelBase> SendFileMessageToUserAsync(IFormFile file)
        {
            var response = new ResponseModelBase();
            var dataJson = Request.Form["data"];

            var message = JsonConvert.DeserializeObject<SaveUserMessageRequestModel>(dataJson!);

            if (message == null)
            {
                response.Message = "Invalida message data.";
            }
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file!.CopyToAsync(memoryStream);
                    response = await _messagesManager.SendMessageToUserAsync(message, memoryStream, file.FileName);
                }
            }
            return response;
        }


        [HttpGet("conversations/{idUser}")]
        public async Task<ResponseModelList<Conversation>> GetUserConversationsAsync(int idUser)
        {
            var response = await _messagesManager.GetUserConversationsAsync(idUser);
            return response;
        }

        [HttpGet("conversation-messages/{idUser}/{idUserFrom}")]
        public async Task<ResponseModelList<UserMessageDto>> GetConversationMessagesAsync(int idUser, int idUserFrom)
        {
            var response = await _messagesManager.GetConversationMessagesAsync(idUser, idUserFrom);
            return response;
        }

        [HttpGet("user-avatar/{idUser}")]
        public async Task<IActionResult> GetUserAvatarAsync(int idUser)
        {
            var buffer = await _messagesManager.GetUserAvatarAsync(idUser);
            return File(buffer, "image/png");
        }

        [HttpGet("proposed-contacts/{idUser}")]
        public async Task<ResponseModelList<ContactInfo>> GetProposedContactsAsync(int idUser)
        {
            var response = await _messagesManager.GetProposedContactsAsync(idUser);
            return response;
        }

        [HttpGet("contacts-search/{companyId}/{keyword}")]
        public async Task<ResponseModelList<ContactInfo>> SearchContactsByKeywordAsync(int companyId, string keyword)
        {
            var response = await _messagesManager.SearchContactsByKeywordAsync(companyId, keyword);
            return response;
        }
    }
}