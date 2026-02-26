using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Messages;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IMessagesRepository
    {
        Task UpdateFcmTokenAsync(int idUser, string token);

        Task<string> GetFcmTokenAsync(int idUser);

        Task<int> InsertMessageAsync(SaveUserMessageRequestModel newMessage);

        Task<List<Conversation>> GetUserConversationsAsync(int idUser);

        Task<List<UserMessageDto>> GetConversationMessagesAsync(int idUser1, int idUser2);

        Task<List<ContactInfo>> GetProposedContactsAsync(int idUser);

        Task<List<ContactInfo>> SearchContactsByKeywordAsync(int companyId, string keyword);

        Task<List<UserMessageDto>> SearchUserMessagesAsync(SearchUserMessagesParams searchParams);
    }
}
