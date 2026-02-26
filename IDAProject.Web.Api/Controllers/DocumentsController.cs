using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsManager _documentsManager;

        public DocumentsController(IDocumentsManager documentsManager)
        {
            _documentsManager = documentsManager;
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadDocumentByIdAsync(int id)
        {
            var documentData = await _documentsManager.GetDocumentDataById(id);

            if (documentData.Valid)
            {
                var documentDownloadData = documentData.Payload!;
                var fileStream = new FileStream(documentDownloadData.FullPath, FileMode.Open);

                return new FileStreamResult(fileStream, documentDownloadData.MimeType)
                {
                    FileDownloadName = documentDownloadData.DownloadFileName
                };
            }
            return BadRequest();
        }


        [HttpPost("upload")]
        public async Task<ResponseModel<int>> UploadDocumentAsync(IFormFile file)
        {
            var dataJson = Request.Form["data"];
            var uploadFileRequestModel = JsonConvert.DeserializeObject<UploadFileRequestModel>(dataJson!);

            using (var memoryStream = new MemoryStream())
            {
                await file!.CopyToAsync(memoryStream);

                var response = await _documentsManager.UploadFileAsync(memoryStream, uploadFileRequestModel!);
                return response;
            }
        }

        [HttpGet("list/{documentType}/{referenceId}")]
        public async Task<ResponseModelList<DocumentDto>> GetDocumentsByReferenceIdAsync(int documentType, int referenceId)
        {
            var result = await _documentsManager.GetDocumentsByReferenceIdAsync(documentType, referenceId);
            return result;
        }

        [HttpDelete("{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteUploadedDocumentAsync(int id, int userId)
        {
            var result = await _documentsManager.DeleteUploadedDocumentAsync(id, userId);
            return result;
        }
    }
}