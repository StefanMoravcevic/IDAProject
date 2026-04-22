
using IDAProject.Web.Models.Dto.Shifts;
using IDAProject.Web.Models.RequestModels.Shifts;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IShiftsRepository
    {
        Task<ShiftDto> GetShiftByIdAsync(int id);
        Task<int> SaveShiftAsync(SaveShiftRequestModel requestModel);
        Task<List<ShiftDto>> SearchShiftsAsync(SearchShiftsParams searchParams);
        Task DeleteShiftAsync(int id, int? userId);
    }
}
