using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.RequestModels.Addresses;

namespace IDAProject.Web.Api.Repositories
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly IDAProjectContext _dbContext;

        public AddressesRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddressDto> GetAddressAsync(int id)
        {
            var address = await (from a in _dbContext.Addresses
                                 where a.Id == id
                                 select DataHelpers.CloneObjectWithIL<Address, AddressDto>(a)
                ).FirstOrDefaultAsync();
            return address!;
        }

        public async Task<AddressDto> GetAddressByIdAsync(int id)
        {
            var searchParams = new SearchAddressesParams
            {
                Id = id
            };
            var result = await SearchAddressesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<AddressDto>> SearchAddressesAsync(SearchAddressesParams searchParams)
        {

            var result = new List<AddressDto>();
            IQueryable<Address> query = _dbContext.Addresses.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.BenefitUserId > 0)
                {
                    query = query.Where(x => x.BenefitUserId! == searchParams.BenefitUserId);
                }
                if (searchParams.PartnerId > 0)
                {
                    query = query.Where(x => x.PartnerId! == searchParams.PartnerId);
                }
                if (searchParams.CompanyId > 0)
                {
                    query = query.Where(x => x.CompanyId! == searchParams.CompanyId);
                }
                if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    query = query.Where(x => x.StreetName!.Contains(searchParams.Keyword));
                }
            }

            result = await query.Select(a => new AddressDto
            {
                Id = a.Id,
                StreetName = a.StreetName,
                StreetNumber = a.StreetNumber,
                ZipCode = a.ZipCode!.ZipCode1,
                City = a.City!.Name,
                State = a.State!.Name,
                Partner = a.Partner!.Name,
                Company = a.Company!.Name,
                CityId = a.CityId,
                StateId = a.StateId,
                PartnerId = a.PartnerId,
                CompanyId = a.CompanyId

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveAddressAsync(SaveAddressRequestModel requestModel)
        {
            Address? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.Addresses.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveAddressRequestModel,Address>(requestModel);
                _dbContext.Addresses.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteAddressAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.Addresses.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.Addresses.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    