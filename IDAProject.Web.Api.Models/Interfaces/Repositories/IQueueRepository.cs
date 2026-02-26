using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.RequestModels.Messages;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IQueueRepository
    {
        Task<int> AddEmailQueueAsync(string subject, string emailTo, string emailCc, string body, bool isBodyHtml);

        Task<IEnumerable<EmailQueueDto>> GetNextEmailQueueAsync(int maxItemsCount);

        Task UpdateEmailQueueAsSentAsync(int id);

        Task<List<EmailDto>> SearchEmailsAsync(SearchEmailsParams searchParams);
    }
}