using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.ScannedLines;
using IDAProject.Web.Models.RequestModels.ScannedLines;

namespace IDAProject.Web.Api.Repositories
{
    public class ScannedLinesRepository : IScannedLinesRepository
    {
        private readonly IDAProjectContext _dbContext;

        public ScannedLinesRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ScannedLineDto> GetScannedLineByIdAsync(int id)
        {
            var searchParams = new SearchScannedLinesParams
            {
                Id = id
            };
            var result = await SearchScannedLinesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<ScannedLineDto>> SearchScannedLinesAsync(SearchScannedLinesParams searchParams)
        {

            var result = new List<ScannedLineDto>();
            IQueryable<ScannedLine> query = _dbContext.ScannedLines;
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.OrderLineId.HasValue)
                {
                    query = query.Where(x => x.OrderLineId == searchParams.OrderLineId);
                }

                if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    var pattern = $"%{searchParams.Keyword}%";
                    query = query.Where(x =>
                    EF.Functions.Like(x.OrderLine.CustomerOrder.CustomerOrderNumber!, pattern) ||
                    EF.Functions.Like(x.OrderLine!.FebiItem.FebiArticleNo!, pattern));
                }
            }

            result = await query.Select(a => new ScannedLineDto
            {
                Id = a.Id,
                ScannedQuantity = a.ScannedQuantity,
                Date = a.Date,
                OrderLineId = a.OrderLineId,
                RequestedQuantity = a.RequestedQuantity,
                UserId = a.UserId,
                CustomerOrderNumber = a.OrderLine.CustomerOrder.CustomerOrderNumber,
                FebiArticleNo = a.OrderLine.FebiItem.FebiArticleNo + " " + a.OrderLine.FebiItem.FebiArticleName

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveScannedLineAsync(SaveScannedLineRequestModel requestModel)
        {
            ScannedLine? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.ScannedLines.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveScannedLineRequestModel,ScannedLine>(requestModel);
                _dbContext.ScannedLines.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteScannedLineAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.ScannedLines.SingleAsync(x => x.Id == id);
            _dbContext.ScannedLines.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    