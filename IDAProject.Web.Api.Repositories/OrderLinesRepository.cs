using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.RequestModels.OrderLines;
using System.Linq.Expressions;

namespace IDAProject.Web.Api.Repositories
{
    public class OrderLinesRepository : IOrderLinesRepository
    {
        private readonly IDAProjectContext _dbContext;

        public OrderLinesRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderLineDto> GetOrderLineByIdAsync(int id)
        {
            var searchParams = new SearchOrderLinesParams
            {
                Id = id
            };
            var result = await SearchOrderLinesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<OrderLineDto>> SearchOrderLinesAsync(SearchOrderLinesParams searchParams)
        {
            var query = _dbContext.OrderLines
                .Where(x => x.IsDeleted == false && x.CustomerOrder.IsArchived == false);

            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.OrderHeaderId.HasValue)
                {
                    query = query.Where(x => x.CustomerOrderId == searchParams.OrderHeaderId);
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

                if (searchParams.SelectedFilters != null && searchParams.SelectedFilters.Any())
                {
                    var filters = searchParams.SelectedFilters
                        .Select(f => new { Date = f.ParsedDate.Date, Segment = f.Segment })
                        .ToList();

                    Expression<Func<OrderLine, bool>> predicate = x => false;

                    foreach (var f in filters)
                    {
                        var date = f.Date;
                        var segment = f.Segment;

                       
                        Expression<Func<OrderLine, bool>> current = x =>
                            x.OrderDate.HasValue && x.OrderDate.Value.Date == date && x.Segment == segment;

                        predicate = OrElse(predicate, current);
                    }

                    query = query.Where(predicate);
                }

                if (searchParams.PartnerCode != null && searchParams.PartnerCode.Any())
                {
                    var partnerCodes = searchParams.PartnerCode.Select(p => p switch
                    {
                        1 => "BEOGRADSKI",
                        2 => "NEBEOGRADSKI",
                        _ => null
                    })
                    .Where(p => p != null)
                    .ToList();

                    if (partnerCodes.Any())
                    {
                        query = query.Where(x => partnerCodes.Contains(x.CustomerOrder.PartnerCode));
                    }
                }
            }

            var result = await query.Select(a => new OrderLineDto
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
                CustomerOrderNumber = a.CustomerOrder.CustomerOrderNumber,
                OrderDate = a.OrderDate,
                Segment = a.Segment,
                DayOfWeek = a.DayOfWeek,
                TourName = a.CustomerOrder.DeliveryRouteCode,
                WintArticleNo = a.FebiItem.WintArticleNo
            }).ToListAsync();

            return result;
        }

        // Helper metoda za spajanje dva Expression<Func<T,bool>> sa OR
        private static Expression<Func<T, bool>> OrElse<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var combined = Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    Expression.Invoke(expr1, parameter),
                    Expression.Invoke(expr2, parameter)
                ),
                parameter
            );

            return combined;
        }

        private string ConvertSegmentToRoman(string segment)
        {
            return segment switch
            {
                "1" => "I",
                "2" => "II",
                "3" => "III",
                _ => segment // ostavi kako jeste ako nije "1", "2", ili "3"
            };
        }
        public async Task<List<OrderLineDto>> SearchArchivedOrderLinesAsync(SearchOrderLinesParams searchParams)
        {
            var query = _dbContext.OrderLines.Where(x => x.IsDeleted == false);

            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.OrderHeaderId.HasValue)
                {
                    query = query.Where(x => x.CustomerOrderId == searchParams.OrderHeaderId);
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
                if (searchParams.Segment != null && searchParams.Segment.Any())
                {
                    var segments = searchParams.Segment.Select(s => s switch
                    {
                        1 => "I",
                        2 => "II",
                        3 => "III",
                        _ => null
                    })
                    .Where(s => s != null)
                    .ToList();

                    if (segments.Any())
                    {
                        query = query.Where(x => segments.Contains(x.Segment));
                    }
                }
                if (searchParams.PartnerCode != null && searchParams.PartnerCode.Any())
                {
                    var partnerCodes = searchParams.PartnerCode.Select(p => p switch
                    {
                        1 => "BEOGRADSKI",
                        2 => "NEBEOGRADSKI",
                        _ => null
                    })
                    .Where(p => p != null)
                    .ToList();

                    if (partnerCodes.Any())
                    {
                        query = query.Where(x => partnerCodes.Contains(x.CustomerOrder.PartnerCode));
                    }
                }


            }

            var result = await query.Select(a => new OrderLineDto
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
                CustomerOrderNumber = a.CustomerOrder.CustomerOrderNumber,
                OrderDate = a.OrderDate,
                Segment = a.Segment,
                DayOfWeek = a.DayOfWeek,
                TourName = a.CustomerOrder.DeliveryRouteCode,
                WintArticleNo = a.FebiItem.WintArticleNo

            }).ToListAsync();

            return result;
        }



        public async Task<int> SaveOrderLineAsync(SaveOrderLineRequestModel requestModel)
        {
            OrderLine? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.OrderLines.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveOrderLineRequestModel,OrderLine>(requestModel);
                _dbContext.OrderLines.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteOrderLineAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.OrderLines.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.OrderLines.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    