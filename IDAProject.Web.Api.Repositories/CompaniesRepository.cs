using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.RequestModels.Companies;

namespace IDAProject.Web.Api.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly IdaContext _dbContext;

        public CompaniesRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<CompanyDto> GetCompanyAsync(int id)
        {
            var company = await (from c in _dbContext.Companies
                                        where c.Id == id
                                        select DataHelpers.CloneObjectWithIL<Company, CompanyDto>(c)
                                        ).FirstOrDefaultAsync();
            return company!;
        }

        public async Task<CompanyDto?> GetCompanyByIdAsync(int id)
        {
            var searchParams = new SearchCompaniesParams
            {
                Id = id
            };

            var result = await SearchCompaniesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<int> SaveCompanyAsync(SaveCompanyRequestModel requestModel)
        {
            Company? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.Companies.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);
            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveCompanyRequestModel, Company>(requestModel);
                _dbContext.Companies.Add(dbRecord!);
            }

            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }
        public async Task DeleteCompanyAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.Companies.SingleAsync(x => x.Id == id);

            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.Companies.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CompanyDto>> SearchCompaniesAsync(SearchCompaniesParams searchParams)
        {
            var result = new List<CompanyDto>();
            IQueryable<Company> query = _dbContext.Companies.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.ParentCompanyId > 0)
                {
                    query = query.Where(x => x.IdParentCompany! == searchParams.ParentCompanyId);
                }

                if (searchParams.FactoringHouseId.HasValue)
                {
                    query = query.Where(x => x.FactoringHouseId == searchParams.FactoringHouseId);
                }
            }

            result = await (from company in query
                            select new CompanyDto
                            {
                                Id = company.Id,
                                Mc = company.Mc,
                                Ein = company.Ein,
                                Email = company.Email,
                                Address = company.Address,
                                WebAddress = company.WebAddress,
                                ResponsiblePerson = company.ResponsiblePerson,
                                StateId = company.StateId,
                                CityId = company.CityId,
                                ZipCodeId = company.ZipCodeId,
                                IdParentCompany = company.IdParentCompany,
                                FactoringHouseId = company.FactoringHouseId,
                                Code = company.Code,
                                Dot = company.Dot,
                                Name = company.Name,
                                Fax = company.Fax,
                                Phone = company.Phone,
                                ZipCode = company.ZipCode!.ZipCode1,
                                State = company.State!.Name,
                                City = company.City!.Name,
                                FactoringHouse = company.FactoringHouse!.Name,
                                ParentCompany = company.IdParentCompanyNavigation!.Name,
                                Logo = company.Logo

                            }).ToListAsync();

            return result;
        }

        #region Org units
        public async Task<OrgUnitDto?> GetOrgUnitByIdAsync(int id)
        {
            var searchParams = new SearchOrgUnitsParams
            {
                Id = id
            };

            var result = await SearchOrgUnitsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<int> SaveOrgUnitAsync(SaveOrgUnitRequestModel requestModel)
        {
            OrgUnit? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.OrgUnits.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);
            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveOrgUnitRequestModel, OrgUnit>(requestModel);
                _dbContext.OrgUnits.Add(dbRecord!);
            }

            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }
        public async Task DeleteOrgUnitAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.OrgUnits.SingleAsync(x => x.Id == id);

            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.OrgUnits.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<OrgUnitDto>> SearchOrgUnitsAsync(SearchOrgUnitsParams searchParams)
        {
            var result = new List<OrgUnitDto>();
            IQueryable<OrgUnit> query = _dbContext.OrgUnits.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.CompanyId > 0)
                {
                    query = query.Where(x => x.CompanyId == searchParams.CompanyId);
                }
                if (searchParams.ParentOrgUnitId > 0)
                {
                    query = query.Where(x => x.ParentOrgUnitId == searchParams.ParentOrgUnitId);
                }

            }

            result = await (from org in query
                            select new OrgUnitDto
                            {
                                Id = org.Id,
                                Code = org.Code,
                                Name = org.Name,
                                CompanyId = org.CompanyId,
                                ParentOrgUnitId = org.ParentOrgUnitId,
                                ParentOrgUnit = org.ParentOrgUnit!.Name,
                                Company = org.Company!.Name

                            }).ToListAsync();

            return result;
        }
        #endregion

    }
}
