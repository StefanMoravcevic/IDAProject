using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using IDAProject.Web.Api.Models.Common;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Documents;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Api.Managers
{
    public class DocumentsManager : IDocumentsManager
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger _logger;
        private readonly FileRepositorySettings _fileRepositorySettings;
        private static readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        static DocumentsManager()
        {
            _fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
        }

        public DocumentsManager(
            ILogger<DocumentsManager> logger,
            IOptions<FileRepositorySettings> options,
            IDocumentsRepository documentsRepository,
            IUsersRepository usersRepository)
        {
            _logger = logger;
            _documentsRepository = documentsRepository;
            _fileRepositorySettings = options.Value;
            _usersRepository = usersRepository;
        }


        public async Task<ResponseModel<int>> UploadFileAsync(MemoryStream memoryStream, UploadFileRequestModel uploadFileRequestModel)
        {
            var result = new ResponseModel<int>();

            try
            {
                uploadFileRequestModel.RelativeFilePath = await GenerateFilePathForNewFileAsync(uploadFileRequestModel);
                var fullFilePath = Path.Combine(_fileRepositorySettings.RootPath, uploadFileRequestModel.RelativeFilePath);

                var fileDirectory = Path.GetDirectoryName(fullFilePath);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory!);
                }

                var fileArray = memoryStream.ToArray();
                using (var fileStream = File.Create(fullFilePath))
                {
                    await fileStream.WriteAsync(fileArray);
                    result.Payload = await _documentsRepository.SaveUploadedFileDataAsync(uploadFileRequestModel);
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(uploadFileRequestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public string GetMimeType(string fileName)
        {
            string contentType;
            _fileExtensionContentTypeProvider.TryGetContentType(fileName, out contentType);
            var result = contentType ?? "application/octet-stream";
            return result;
        }

        public async Task<ResponseModel<DocumentDownloadData>> GetDocumentDataById(int id)
        {
            var result = new ResponseModel<DocumentDownloadData>();
            try
            {
                result.Payload = await _documentsRepository.GetDocumentDownloadDataById(id);
                result.Payload.FullPath = Path.Combine(_fileRepositorySettings.RootPath, result.Payload.RelativeFilePath);
                result.Payload.MimeType = GetMimeType(result.Payload.DownloadFileName);

                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<byte[]>> GetDocumentById(int id)
        {
            var docData = await _documentsRepository.GetDocumentDownloadDataById(id);
            var docFullPath = Path.Combine(_fileRepositorySettings.RootPath, docData.RelativeFilePath);

            var result = new ResponseModel<byte[]>();
            try
            {
                result.Payload = File.ReadAllBytes(docFullPath);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        private async Task<string> GenerateFilePathForNewFileAsync(UploadFileRequestModel documentData)
        {
            var companyId = await _usersRepository.GetUserCompanyIdAsync(documentData.UserId);

            var companyFolderName = companyId.ToString("F0", CultureInfo.InvariantCulture);
            var documentTypeFolderName = documentData.DocumentTypeId.ToString("F0", CultureInfo.InvariantCulture);

            var fileExtension = Path.GetExtension(documentData.FileName);
            var physicalFileName = $"{Guid.NewGuid()}{fileExtension}";

            var filePath = Path.Combine(companyFolderName, documentTypeFolderName, physicalFileName);

            return filePath;
        }

        public async Task<ResponseModelList<DocumentDto>> GetDocumentsByReferenceIdAsync(int documentType, int referenceId)
        {
            var result = new ResponseModelList<DocumentDto>();
            try
            {
                result.Payload = await _documentsRepository.GetDocumentsByReferenceIdAsync(documentType, referenceId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"documentType: {documentType}, referenceId:{referenceId}");
            }
            return result;
        }

        public async Task<ResponseModelBase> DeleteUploadedDocumentAsync(int id, int userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _documentsRepository.DeleteUploadedDocumentAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}, userId:{userId}");
            }
            return result;
        }
    }
}