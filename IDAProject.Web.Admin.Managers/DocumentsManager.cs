using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Managers
{
    public class DocumentsManager : BaseManager, IDocumentsManager
    {
        public DocumentsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DocumentsManager> logger,IHttpContextAccessor httpContextAccessor )
            : base(httpClientFactory, configuration, logger)
        {
        }

        public Task<ResponseModel<DocumentDownloadData>> GetDocumentDataById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelList<DocumentDto>> GetDocumentsByReferenceIdAsync(int documentType, int referenceId)
        {
            var result = await GetAsync<ResponseModelList<DocumentDto>>($"api/documents/list/{documentType}/{referenceId}");
            return result;
        }

        public async Task<ResponseModel<int>> UploadDocumentAsync(MemoryStream stream, UploadFileRequestModel uploadFileRequestModel)
        {
            var result = new ResponseModel<int>();
            var httpClient = GetHttpClient();
            var endpointPath = GetEndpointUrl("api/documents/upload");

            var form = new MultipartFormDataContent();

            var requestModelJson = JsonConvert.SerializeObject(uploadFileRequestModel);
            form.Add(new StringContent(requestModelJson, Encoding.UTF8, "application/json"), "data");
            form.Add(new ByteArrayContent(stream.ToArray()), "file", uploadFileRequestModel.FileName);

            var response = await httpClient.PostAsync(endpointPath, form);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<ResponseModel<int>>();
            }
            return result;
        }

        public async Task<ResponseModelBase> DeleteUploadedDocumentAsync(int id, int userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/documents/{id}/{userId}");
            return result;
        }
    }
}