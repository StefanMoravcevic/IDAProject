using IDAProject.Web.Models.Dto.DocumentSeries;
using IDAProject.Web.Models.RequestModels.DocumentSeries;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IDocumentSeriesRepository
    {
        Task<List<DocumentSerieDto>> SearchDocumentSeriesAsync(SearchDocumentSeriesParams searchParams);

        Task<DocumentSerieDto?> GetDocumentSerieByIdAsync(int id);
        Task DeleteDocumentSerieAsync(int id, int? userId);

        Task<int> SaveDocumentSerieAsync(SaveDocumentSerieRequestModel requestModel);

        Task<string> GetNewNumberAsync(int documentSerieTypeId);

    }
}
