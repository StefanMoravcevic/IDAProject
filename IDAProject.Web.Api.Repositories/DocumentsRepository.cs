using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Models.Dto.Documents;

namespace IDAProject.Web.Api.Repositories
{
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly IdaContext _dbContext;

        public DocumentsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveUploadedFileDataAsync(UploadFileRequestModel uploadFileRequestModel)
        {
            List<int> validSources = new List<int> { 1, 2, 3 };
            if (!validSources.Contains(uploadFileRequestModel.SourceId))
            {
                uploadFileRequestModel.SourceId = 1;
            }

            var documentRecord = new Document
            {
                DocumentTypeId = uploadFileRequestModel.DocumentTypeId,
                ReferenceId = uploadFileRequestModel.ReferenceId,
                IsDeleted = false,
                RelativeFilePath = uploadFileRequestModel.RelativeFilePath,
                UploadedDate = DateTime.UtcNow,
                DownloadFileName = uploadFileRequestModel.FileName,
                UploadedByUserId = uploadFileRequestModel.UserId,
                SourceId = uploadFileRequestModel.SourceId
            };

            _dbContext.Documents.Add(documentRecord);
            await _dbContext.SaveChangesAsync();
            return documentRecord.Id;
        }

        public async Task<DocumentDownloadData> GetDocumentDownloadDataById(int id)
        {
            var dbRecord = await _dbContext.Documents.SingleAsync(x => x.Id == id);

            var result = new DocumentDownloadData
            {
                DownloadFileName = dbRecord.DownloadFileName,
                RelativeFilePath = dbRecord.RelativeFilePath
            };
            return result;
        }

        public async Task<List<DocumentDto>> GetDocumentsByReferenceIdAsync(int documentType, int referenceId)
        {
            var query = from doc in _dbContext.Documents
                         where doc.IsDeleted == false && doc.DocumentTypeId == documentType && doc.ReferenceId == referenceId
                         orderby doc.UploadedDate descending
                         select new DocumentDto
                         {
                             Id = doc.Id,
                             DocumentTypeId = doc.DocumentTypeId,
                             ReferenceId = doc.ReferenceId,
                             DownloadFileName = doc.DownloadFileName,
                             RelativeFilePath = doc.RelativeFilePath,
                             UploadedDate = doc.UploadedDate,
                             DocumentType = doc.DocumentType.Name,
                             UploadedBy = doc.UploadedByUser.UserName,
                             UploadedByUserId = doc.UploadedByUserId,
                             SourceId = doc.SourceId
                         };

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<List<DocumentDto>> GetDocumentsByReferenceIdAsync(List<int> documentTypes, int referenceId)
        {
            var query = from doc in _dbContext.Documents
                        where doc.IsDeleted == false && documentTypes.Contains(doc.DocumentTypeId) && doc.ReferenceId == referenceId
                        orderby doc.UploadedDate descending
                        select new DocumentDto
                        {
                            Id = doc.Id,
                            DocumentTypeId = doc.DocumentTypeId,
                            ReferenceId = doc.ReferenceId,
                            DownloadFileName = doc.DownloadFileName,
                            RelativeFilePath = doc.RelativeFilePath,
                            UploadedDate = doc.UploadedDate,
                            DocumentType = doc.DocumentType.Name,
                            UploadedBy = doc.UploadedByUser.UserName,
                            UploadedByUserId = doc.UploadedByUserId,
                            SourceId = doc.SourceId
                        };

            var result = await query.ToListAsync();
            return result;
        }

        public async Task DeleteUploadedDocumentAsync(int id, int userId)
        {
            var dbRecord = await _dbContext.Documents.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }
    }
}