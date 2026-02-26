using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.FebiItems;
using IDAProject.Web.Models.RequestModels.FebiItems;

namespace IDAProject.Web.Api.Repositories
{
    public class FebiItemsRepository : IFebiItemsRepository
    {
        private readonly IDAProjectContext _dbContext;

        public FebiItemsRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FebiItemDto> GetFebiItemByIdAsync(int id)
        {
            var searchParams = new SearchFebiItemsParams
            {
                Id = id
            };
            var result = await SearchFebiItemsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<FebiItemDto>> SearchFebiItemsAsync(SearchFebiItemsParams searchParams)
        {
            var query = _dbContext.FebiItems.Where(x => x.IsDeleted == false);

            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                // OR filter samo ako barCode ili articleNo imaju vrednost
                if (!string.IsNullOrEmpty(searchParams.ArticleNo) || !string.IsNullOrEmpty(searchParams.BarCode))
                {
                    query = query.Where(x =>
                        (!string.IsNullOrEmpty(searchParams.ArticleNo) && x.FebiArticleNo == searchParams.ArticleNo) ||
                        (!string.IsNullOrEmpty(searchParams.BarCode) && x.BarCode == searchParams.BarCode)
                    );
                }

                // Keyword filter
                if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    var keywordUpper = searchParams.Keyword.ToUpper();
                    query = query.Where(x =>
                        x.FebiArticleName.ToUpper().Contains(keywordUpper) ||
                        x.FebiArticleNo.ToUpper().Contains(keywordUpper) ||
                        x.WintArticleNo.ToUpper().Contains(keywordUpper) 
                    );
                }
            }

            return await query.Select(a => new FebiItemDto
            {
                Id = a.Id,
                FebiArticleName = a.FebiArticleName,
                FebiArticleNo = a.FebiArticleNo,
                FebiPackingUnit = a.FebiPackingUnit,
                BarCode = a.BarCode,
                WintArticleNo = a.WintArticleNo
            }).ToListAsync();
        }

        public async Task<int> SaveFebiItemAsync(SaveFebiItemRequestModel requestModel)
        {
            FebiItem? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.FebiItems.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveFebiItemRequestModel,FebiItem>(requestModel);
                _dbContext.FebiItems.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteFebiItemAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.FebiItems.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.FebiItems.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    