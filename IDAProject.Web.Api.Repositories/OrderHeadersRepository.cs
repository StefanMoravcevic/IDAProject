using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.OrderHeaders;
using IDAProject.Web.Models.RequestModels.OrderHeaders;

namespace IDAProject.Web.Api.Repositories
{
    public class OrderHeadersRepository : IOrderHeadersRepository
    {
        private readonly IDAProjectContext _dbContext;

        public OrderHeadersRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderHeaderDto> GetOrderHeaderByIdAsync(int id)
        {
            var searchParams = new SearchOrderHeadersParams
            {
                Id = id
            };
            var result = await SearchOrderHeadersAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<OrderHeaderDto>> SearchOrderHeadersAsync(SearchOrderHeadersParams searchParams)
        {

            var result = new List<OrderHeaderDto>();
            IQueryable<OrderHeader> query = _dbContext.OrderHeaders.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.IsArchived.HasValue)
                {
                    query = query.Where(x => x.IsArchived == searchParams.IsArchived);
                }
                if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    var pattern = $"%{searchParams.Keyword}%";
                    query = query.Where(x =>
                    EF.Functions.Like(x.CustomerOrderNumber!, pattern) ||
                    EF.Functions.Like(x.PartnerCode!, pattern) ||
                    EF.Functions.Like(x.DeliveryRouteCode!, pattern)
                    );
                }
            }

            result = await query.Select(a => new OrderHeaderDto
            {
                Id = a.Id,
                CreatedDate = a.CreatedDate,
                CustomerOrderNumber = a.CustomerOrderNumber,
                DeliveryRouteCode = a.DeliveryRouteCode,
                PartnerCode = a.PartnerCode

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveOrderHeaderAsync(SaveOrderHeaderRequestModel requestModel)
        {
            OrderHeader? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.OrderHeaders.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveOrderHeaderRequestModel,OrderHeader>(requestModel);
                _dbContext.OrderHeaders.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteOrderHeaderAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.OrderHeaders.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.OrderHeaders.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    