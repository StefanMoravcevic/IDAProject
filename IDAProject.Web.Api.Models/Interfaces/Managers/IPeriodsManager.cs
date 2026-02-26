using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.Periods;
using IDAProject.Web.Models.RequestModels.Periods;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IPeriodsManager
    {
        Task<ResponseModelList<PeriodDto>> SearchPeriodsAsync(SearchPeriodsParams searchParams);
        Task<ResponseModel<PeriodDto>> GetPeriodByIdAsync(int id);
        Task<ResponseModelBase> DeletePeriodAsync(int id, int? userId);
        Task<ResponseModel<int>> SavePeriodAsync(SavePeriodRequestModel requestModel);
    }
}
