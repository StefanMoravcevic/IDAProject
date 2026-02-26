using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.RequestModels.OrderHeaderArchives;

namespace IDAProject.Web.Api.Repositories
{
    public class OrderHeaderArchivesRepository : IOrderHeaderArchivesRepository
    {
        private readonly IDAProjectContext _dbContext;

        public OrderHeaderArchivesRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderHeaderArchiveDto> GetOrderHeaderArchiveByIdAsync(int id)
        {
            var searchParams = new SearchOrderHeaderArchivesParams
            {
                Id = id
            };
            var result = await SearchOrderHeaderArchivesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<OrderHeaderArchiveDto>> SearchOrderHeaderArchivesAsync(SearchOrderHeaderArchivesParams searchParams)
        {

            var result = new List<OrderHeaderArchiveDto>();
            IQueryable<OrderHeaderArchive> query = _dbContext.OrderHeaderArchives.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    var pattern = $"%{searchParams.Keyword}%";
                    query = query.Where(x =>
                    EF.Functions.Like(x.OrderHeader.CustomerOrderNumber!, pattern) || 
                    EF.Functions.Like(x.PartnerCode!, pattern) ||
                    EF.Functions.Like(x.DeliveryRouteCode!, pattern)
                    );
                }
            }

            result = await query.Select(a => new OrderHeaderArchiveDto
            {
                Id = a.Id,
                CreatedTime = a.CreatedTime,
                CreatedDate = a.CreatedDate,
                DeliveryRouteCode = a.DeliveryRouteCode,
                PartnerCode = a.PartnerCode,
                CustomerOrderNumber = a.OrderHeader.CustomerOrderNumber
                

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveOrderHeaderArchiveAsync(SaveOrderHeaderArchiveRequestModel requestModel)
        {
            OrderHeaderArchive? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.OrderHeaderArchives.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveOrderHeaderArchiveRequestModel,OrderHeaderArchive>(requestModel);
                _dbContext.OrderHeaderArchives.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteOrderHeaderArchiveAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.OrderHeaderArchives.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.OrderHeaderArchives.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    