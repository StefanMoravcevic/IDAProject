
using IDAProject.Web.Models.Dto.FebiItems;
using IDAProject.Web.Models.RequestModels.FebiItems;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IFebiItemsRepository
    {
        Task<FebiItemDto> GetFebiItemByIdAsync(int id);
        Task<int> SaveFebiItemAsync(SaveFebiItemRequestModel requestModel);
        Task<List<FebiItemDto>> SearchFebiItemsAsync(SearchFebiItemsParams searchParams);
        Task DeleteFebiItemAsync(int id, int? userId);
    }
}
