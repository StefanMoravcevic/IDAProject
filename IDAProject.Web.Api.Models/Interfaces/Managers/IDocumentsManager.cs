using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IDocumentsManager
    {
        Task<ResponseModel<int>> UploadFileAsync(MemoryStream memoryStream, UploadFileRequestModel uploadFileRequestModel);

        Task<ResponseModel<DocumentDownloadData>> GetDocumentDataById(int id);

        Task<ResponseModel<byte[]>> GetDocumentById(int id);

        Task<ResponseModelList<DocumentDto>> GetDocumentsByReferenceIdAsync(int documentType, int referenceId);

        Task<ResponseModelBase> DeleteUploadedDocumentAsync(int id, int userId);

        string GetMimeType(string fileName);
    }
}
