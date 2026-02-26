using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IDocumentsManager
    {
        Task<ResponseModel<DocumentDownloadData>> GetDocumentDataById(int id);

        Task<ResponseModelList<DocumentDto>> GetDocumentsByReferenceIdAsync(int documentType, int referenceId);

        Task<ResponseModel<int>> UploadDocumentAsync(MemoryStream stream, UploadFileRequestModel uploadFileRequestModel);

        Task<ResponseModelBase> DeleteUploadedDocumentAsync(int id, int userId);
    }
}
