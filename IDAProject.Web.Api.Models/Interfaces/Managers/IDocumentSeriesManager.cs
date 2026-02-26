using IDAProject.Web.Models.Dto.DocumentSeries;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.DocumentSeries;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IDocumentSeriesManager
    {
        Task<ResponseModelList<DocumentSerieDto>> SearchDocumentSeriesAsync(SearchDocumentSeriesParams searchParams);

        Task<ResponseModel<DocumentSerieDto>> GetDocumentSerieByIdAsync(int id);
        Task<ResponseModelBase> DeleteDocumentSerieAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveDocumentSerieAsync(SaveDocumentSerieRequestModel requestModel);
        Task<ResponseModel<string>> GetNewNumberAsync(int documentSerieTypeId);

    }
}
