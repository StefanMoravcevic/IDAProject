using IDAProject.Web.Models.Dto.Documents;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IDocumentsRepository
    {
        Task<int> SaveUploadedFileDataAsync(UploadFileRequestModel uploadFileRequestModel);

        Task<DocumentDownloadData> GetDocumentDownloadDataById(int id);

        Task<List<DocumentDto>> GetDocumentsByReferenceIdAsync(int documentType, int referenceId);

        Task<List<DocumentDto>> GetDocumentsByReferenceIdAsync(List<int> documentTypes, int referenceId);

        Task DeleteUploadedDocumentAsync(int id, int userId);
    }
}
