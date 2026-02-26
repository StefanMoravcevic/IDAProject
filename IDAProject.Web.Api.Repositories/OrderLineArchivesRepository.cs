using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.RequestModels.OrderLineArchives;

namespace IDAProject.Web.Api.Repositories
{
    public class OrderLineArchivesRepository : IOrderLineArchivesRepository
    {
        private readonly IDAProjectContext _dbContext;

        public OrderLineArchivesRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderLineArchiveDto> GetOrderLineArchiveByIdAsync(int id)
        {
            var searchParams = new SearchOrderLineArchivesParams
            {
                Id = id
            };
            var result = await SearchOrderLineArchivesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<OrderLineArchiveDto>> SearchOrderLineArchivesAsync(SearchOrderLineArchivesParams searchParams)
        {

            var result = new List<OrderLineArchiveDto>();
            IQueryable<OrderLineArchive> query = _dbContext.OrderLineArchives.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.OrderHeaderArchiveId.HasValue)
                {
                    query = query.Where(x => x.CustomerOrderId == searchParams.OrderHeaderArchiveId);
                }
                if (searchParams.ContextOrderHeaderId != null && searchParams.ContextOrderHeaderId.Length > 0)
                {
                    query = query.Where(x => x.CustomerOrderId > 0 && searchParams.ContextOrderHeaderId.Contains(x.CustomerOrderId));
                }
                if (searchParams.ArticleId.HasValue)
                {
                    query = query.Where(x => x.FebiItemId == searchParams.ArticleId);
                }
                if (searchParams.FebiItemId != null && searchParams.FebiItemId.Length > 0)
                {
                    query = query.Where(x => x.FebiItemId > 0 && searchParams.FebiItemId.Contains(x.FebiItemId.Value));
                }
                if (searchParams.DateFrom.HasValue)
                {
                    query = query.Where(x => x.OrderDate >= searchParams.DateFrom);
                }
                if (searchParams.DateTo.HasValue)
                {
                    query = query.Where(x => x.OrderDate <= searchParams.DateTo);
                }
                if (searchParams.Segment.HasValue)
                {
                    string segment = searchParams.Segment.Value switch
                    {
                        1 => "I",
                        2 => "II",
                        3 => "III",
                        _ => null
                    };

                    if (segment != null)
                    {
                        query = query.Where(x => x.Segment == segment);
                    }
                }
                if (searchParams.PartnerCode.HasValue)
                {
                    string partnerCode = searchParams.PartnerCode.Value switch
                    {
                        1 => "BEOGRADSKI",
                        2 => "NEBEOGRADSKI",
                        _ => null
                    };

                    if (partnerCode != null)
                    {
                        query = query.Where(x => x.CustomerOrder.PartnerCode == partnerCode);
                    }
                }

            }

            result = await query.Select(a => new OrderLineArchiveDto
            {
                Id = a.Id,
                CheckedQuantity = a.CheckedQuantity,
                CustomerOrderId = a.CustomerOrderId,
                FebiArticleName = a.FebiItem.FebiArticleName,
                FebiArticleNo = a.FebiItem.FebiArticleNo,
                FebiArticlePackingUnit = a.FebiItem.FebiPackingUnit,
                FebiItemId = a.FebiItemId,
                LineNo = a.LineNo,
                PartnerCode = a.CustomerOrder.PartnerCode,
                RequestedQuantity = a.RequestedQuantity,
                CustomerOrderNumber = a.CustomerOrder.OrderHeader.CustomerOrderNumber,
                OrderDate = a.OrderDate,
                Segment = a.Segment,
                DayOfWeek = a.DayOfWeek,
                TourName = a.CustomerOrder.DeliveryRouteCode,
                WintArticleNo = a.FebiItem.WintArticleNo

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveOrderLineArchiveAsync(SaveOrderLineArchiveRequestModel requestModel)
        {
            OrderLineArchive? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.OrderLineArchives.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveOrderLineArchiveRequestModel,OrderLineArchive>(requestModel);
                _dbContext.OrderLineArchives.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteOrderLineArchiveAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.OrderLineArchives.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.OrderLineArchives.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    