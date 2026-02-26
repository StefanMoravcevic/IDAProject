using Microsoft.EntityFrameworkCore;
using System.Data;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.DocumentSeries;
using IDAProject.Web.Models.RequestModels.DocumentSeries;

namespace IDAProject.Web.Api.Repositories
{
    public class DocumentSeriesRepository : IDocumentSeriesRepository
    {
        private readonly IDAProjectContext _dbContext;

        public DocumentSeriesRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DocumentSerieDto?> GetDocumentSerieByIdAsync(int id)
        {
            var searchParams = new SearchDocumentSeriesParams
            {
                Id = id
            };

            var result = await SearchDocumentSeriesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<int> SaveDocumentSerieAsync(SaveDocumentSerieRequestModel requestModel)
        {
            DocumentSeries? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.DocumentSeries.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);
            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveDocumentSerieRequestModel, DocumentSeries>(requestModel);
                _dbContext.DocumentSeries.Add(dbRecord!);
            }

            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }
        public async Task DeleteDocumentSerieAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.DocumentSeries.SingleAsync(x => x.Id == id);

            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.DocumentSeries.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DocumentSerieDto>> SearchDocumentSeriesAsync(SearchDocumentSeriesParams searchParams)
        {
            var result = new List<DocumentSerieDto>();
            IQueryable<DocumentSeries> query = _dbContext.DocumentSeries.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.DocumentSerieTypeId.HasValue)
                {
                    query = query.Where(x => x.DocumentSerieTypeId == searchParams.DocumentSerieTypeId);
                }
            }

            result = await (from docS in query
                            select new DocumentSerieDto
                            {
                                Id = docS.Id,
                                Year = docS.Year,
                                Pattern = docS.Pattern,
                                NextNumber = docS.NextNumber,
                                DocumentSerieTypeId = docS.DocumentSerieTypeId,
                                DocumentSerieType = docS.DocumentSerieType.Name!,
                                IncrementSeed = docS.IncrementSeed

                            }).ToListAsync();

            return result;
        }
        public async Task<string> GetNewNumberAsync(int documentSerieTypeId)
        {
            DocumentSeries? dbRecord;
            string? response = string.Empty;
            IQueryable<DocumentSeries> query = _dbContext.DocumentSeries.Where(x => x.IsDeleted == false && x.DocumentSerieTypeId == documentSerieTypeId && x.Year == DateTime.Now.Year);
            if (query.Count() == 0)
            {
                query = _dbContext.DocumentSeries.Where(x => x.IsDeleted == false && x.DocumentSerieTypeId == documentSerieTypeId && x.Year == DateTime.Now.Year - 1);
                if (query.Count() == 0)
                {
                    var saveModel = new SaveDocumentSerieRequestModel()
                    {
                        DocumentSerieTypeId = documentSerieTypeId,
                        IncrementSeed = 1,
                        NextNumber = 2,
                        Year = DateTime.Now.Year
                    };
                    dbRecord = DataHelpers.CloneObjectWithIL<SaveDocumentSerieRequestModel, DocumentSeries>(saveModel);
                }
                else
                {
                    var saveModel = new SaveDocumentSerieRequestModel()
                    {
                        DocumentSerieTypeId = documentSerieTypeId,
                        IncrementSeed = query.FirstOrDefault()!.IncrementSeed,
                        NextNumber = 2,
                        Year = DateTime.Now.Year,
                        Pattern = query.FirstOrDefault()!.Pattern
                    };
                    dbRecord = DataHelpers.CloneObjectWithIL<SaveDocumentSerieRequestModel, DocumentSeries>(saveModel);
                }
                _dbContext.DocumentSeries.Add(dbRecord!);
				response = query.FirstOrDefault()!.Pattern == null ? query.FirstOrDefault()!.NextNumber!.ToString() : query.FirstOrDefault()!.Pattern!
					.Replace("*", 1.ToString())
					.Replace("#", 1.ToString())
					.Replace("$", 1.ToString())
					.Replace("%", 1.ToString())
					.Replace("yyyy", DateTime.Now.Year.ToString())
					.Replace("YYYY", DateTime.Now.Year.ToString())
					.Replace("yy", DateTime.Now.Year.ToString().Substring(2, 2))
					.Replace("YY", DateTime.Now.Year.ToString().Substring(2, 2));
			}
			else
            {
                response = query.FirstOrDefault()!.Pattern == null ? query.FirstOrDefault()!.NextNumber!.ToString() : query.FirstOrDefault()!.Pattern!
                    .Replace("*", query.FirstOrDefault()!.NextNumber!.ToString())
                    .Replace("#", query.FirstOrDefault()!.NextNumber!.ToString())
                    .Replace("$", query.FirstOrDefault()!.NextNumber!.ToString())
                    .Replace("%", query.FirstOrDefault()!.NextNumber!.ToString())
                    .Replace("yyyy", DateTime.Now.Year.ToString())
                    .Replace("YYYY", DateTime.Now.Year.ToString())
                    .Replace("yy", DateTime.Now.Year.ToString().Substring(2,2))
                    .Replace("YY", DateTime.Now.Year.ToString().Substring(2,2));
                dbRecord = await _dbContext.DocumentSeries.SingleAsync(x => x.Id == query.FirstOrDefault()!.Id);
                dbRecord.NextNumber += dbRecord.IncrementSeed;
                _dbContext.DocumentSeries.Update(dbRecord);

            }
            await _dbContext.SaveChangesAsync();
            return response!;
        }

    }
}