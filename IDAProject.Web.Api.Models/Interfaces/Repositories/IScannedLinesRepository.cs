
using IDAProject.Web.Models.Dto.ScannedLines;
using IDAProject.Web.Models.RequestModels.ScannedLines;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IScannedLinesRepository
    {
        Task<ScannedLineDto> GetScannedLineByIdAsync(int id);
        Task<int> SaveScannedLineAsync(SaveScannedLineRequestModel requestModel);
        Task<List<ScannedLineDto>> SearchScannedLinesAsync(SearchScannedLinesParams searchParams);
        Task DeleteScannedLineAsync(int id, int? userId);
    }
}
