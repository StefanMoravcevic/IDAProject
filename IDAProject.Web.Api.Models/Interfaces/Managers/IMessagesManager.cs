using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Messages;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IMessagesManager
    {
        Task<ResponseModel<int>> SendMessageToUserAsync(SaveUserMessageRequestModel message, MemoryStream? fileContent, string fileName);


        Task<ResponseModelBase> UpdateFcmTokenAsync(int idUser, string token);

        Task<ResponseModelList<Conversation>> GetUserConversationsAsync(int idUser);

        Task<ResponseModelList<UserMessageDto>> GetConversationMessagesAsync(int idUser, int idUserFrom);

        Task<byte[]> GetUserAvatarAsync(int idUser);

        Task<ResponseModelList<ContactInfo>> GetProposedContactsAsync(int idUser);

        Task<ResponseModelList<ContactInfo>> SearchContactsByKeywordAsync(int companyId, string keyword);

        Task<ResponseModelList<UserMessageDto>> SearchUserMessagesAsync(SearchUserMessagesParams searchParams);
    }
}
