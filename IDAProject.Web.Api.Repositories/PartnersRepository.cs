using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Partners;

namespace IDAProject.Web.Api.Repositories
{
    public class PartnersRepository : IPartnersRepository
    {
        private readonly IDAProjectContext _dbContext;

        public PartnersRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PartnerDto?> GetPartnerByIdAsync(int id)
        {
            var searchParams = new SearchPartnersParams
            {
                Id = id
            };

            var result = await SearchPartnersAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<int> SavePartnerAsync(SavePartnerRequestModel requestModel)
        {
            Partner? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.Partners.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);
            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SavePartnerRequestModel, Partner>(requestModel);
                _dbContext.Partners.Add(dbRecord!);
            }

            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task<List<PartnerDto>> SearchPartnersAsync(SearchPartnersParams searchParams)
        {
            var result = new List<PartnerDto>();
            var query = from p in _dbContext.Partners
                        where p.IsDeleted == false
                        select new PartnerDto
                        {
                            Id = p.Id,
                            AccountingCode = p.AccountingCode,
                            PartnerTypeId = p.PartnerTypeId,
                            PartnerType = p.PartnerType.Name,
                            PartnerCategorie = p.PartnerType.PartnerCategory.Name,
                            Address = p.Address,
                            Blocked = p.Blocked,
                            BlockedComment = p.BlockedComment,
                            Code = p.Code,
                            BankAccountNumber = p.BankAccountNumber,
                            RautingNumber = p.RautingNumber,
                            ContactPerson = p.ContactPerson,
                            Dot = p.Dot,
                            Ein = p.Ein,
                            Email = p.Email,
                            Fax = p.Fax,
                            Mc = p.Mc,
                            Phone = p.Phone,
                            ZipCodeId = p.ZipCodeId,
                            ZipCode = p.ZipCode!.ZipCode1,
                            StateId = p.StateId,
                            State = p.State!.Name,
                            StateShort = p.State!.ShortName,
                            CityId = p.CityId,
                            City = p.City!.Name,
                            PartnerCategoryId = p.PartnerType.PartnerCategoryId,
                            Name = p.Name,
                            DisclaimerNote = p.DisclaimerNote
                        };

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
                    EF.Functions.Like(x.Code!, pattern) ||
                    EF.Functions.Like(x.Name, pattern) ||
                    EF.Functions.Like(x.Address!, pattern));
                }

                if (searchParams.PartnerCategoryId.HasValue)
                {
                    if (searchParams.PartnerCategoryId == 9999)
                    {
                        query = query.Where(x => x.PartnerCategoryId != PartnerTypes.Brokers && x.PartnerCategoryId != PartnerTypes.Suppliers &&
                        x.PartnerCategoryId != PartnerTypes.Subcontractors && x.PartnerCategoryId != PartnerTypes.Customers &&
                        x.PartnerCategoryId != PartnerTypes.Factoring_houses && x.PartnerCategoryId != PartnerTypes.Lessees);
                    }
                    else
                    {
                        query = query.Where(x => x.PartnerCategoryId == searchParams.PartnerCategoryId.Value);
                    }
                }
                if (searchParams.Blocked.HasValue)
                {
                    query = query.Where(x => x.Blocked == searchParams.Blocked.Value);
                }
            }

            result = await query.ToListAsync();

            return result;
        }

        public async Task DeletePartnerAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.Partners.SingleAsync(x => x.Id == id);

            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.Partners.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<GenericSelectOption>> GetPartnersOptionsByCategoryAsync(int partnerCategory)
        {
            var query = from par in _dbContext.Partners
                        where par.IsDeleted == false
                        select new PartnerDto
                        {
                            Id = par.Id,
                            Name = par.Name,
                            PartnerTypeId = par.PartnerTypeId,
                            PartnerCategoryId = par.PartnerType.PartnerCategoryId,
                            Code = par.Code
                        };
            var query1 = from records in query
                         where records.PartnerCategoryId == partnerCategory
                         orderby records.Name descending
                         select new GenericSelectOption
                         {
                             Value = records.Id,
                             Description = records.Name
                         };
            var result = await query1.ToListAsync();
            return result;
        }
    }
}