using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class DocumentsController : BaseController
    {
        private readonly IDocumentsManager _documentsManager;

        public DocumentsController(ILogger<DocumentsController> logger, IAccountManager accountManager, IDocumentsManager documentsManager)
            : base(accountManager, logger)
        {
            _documentsManager = documentsManager;
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [HttpGet("list/{documentType}/{referenceId}", Name = RouteNames.Documents_List)]
        public async Task<List<DocumentDto>> GetDocumentsByReferenceIdAsync(int documentType, int referenceId)
        {
            var result = await _documentsManager.GetDocumentsByReferenceIdAsync(documentType, referenceId);
            return result.Payload;
        }


        [HttpPost("upload", Name = RouteNames.Documents_Upload)]
        public async Task<ResponseModel<int>> UploadDocumentsAsync(UploadFileRequestModel documentData)
        {
            var file = Request.Form.Files.FirstOrDefault();
            var currentUser = GetCurrentUser();

            documentData.FileName = file!.FileName;
            documentData.UserId = currentUser.Id;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                var result = await _documentsManager.UploadDocumentAsync(memoryStream, documentData);
                return result;
            }
        }

        [HttpDelete("{id}", Name = RouteNames.Documents_Delete)]
        public async Task<ResponseModelBase> DeleteUploadedDocumentAsync(int id)
        {
            var currentUser = GetCurrentUser();
            var result = await _documentsManager.DeleteUploadedDocumentAsync(id, currentUser.Id);
            return result;
        }
    }
}