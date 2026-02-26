
using IDAProject.Web.Models.Dto.Periods;
using IDAProject.Web.Models.RequestModels.Periods;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IPeriodsRepository
    {
        Task<PeriodDto> GetPeriodByIdAsync(int id);
        Task<int> SavePeriodAsync(SavePeriodRequestModel requestModel);
        Task<List<PeriodDto>> SearchPeriodsAsync(SearchPeriodsParams searchParams);
        Task DeletePeriodAsync(int id, int? userId);
    }
}
