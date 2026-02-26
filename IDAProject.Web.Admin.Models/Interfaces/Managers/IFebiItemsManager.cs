using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.FebiItems;
using IDAProject.Web.Models.RequestModels.FebiItems;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IFebiItemsManager
    {
        Task<ResponseModelList<FebiItemDto>> SearchFebiItemsAsync(SearchFebiItemsParams searchParams);
        Task<ResponseModel<FebiItemDto>> GetFebiItemByIdAsync(int id);
        Task<ResponseModelBase> DeleteFebiItemAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveFebiItemAsync(SaveFebiItemRequestModel requestModel);
    }
}

