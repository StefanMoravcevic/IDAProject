using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.Shifts;
using IDAProject.Web.Models.RequestModels.Shifts;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IShiftsManager
    {
        Task<ResponseModelList<ShiftDto>> SearchShiftsAsync(SearchShiftsParams searchParams);
        Task<ResponseModel<ShiftDto>> GetShiftByIdAsync(int id);
        Task<ResponseModelBase> DeleteShiftAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveShiftAsync(SaveShiftRequestModel requestModel);
    }
}
