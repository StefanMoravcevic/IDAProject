using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.ScannedLines;
using IDAProject.Web.Models.RequestModels.ScannedLines;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IScannedLinesManager
    {
        Task<ResponseModelList<ScannedLineDto>> SearchScannedLinesAsync(SearchScannedLinesParams searchParams);
        Task<ResponseModel<ScannedLineDto>> GetScannedLineByIdAsync(int id);
        Task<ResponseModelBase> DeleteScannedLineAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveScannedLineAsync(SaveScannedLineRequestModel requestModel);
    }
}

