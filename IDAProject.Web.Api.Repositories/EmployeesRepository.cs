using Microsoft.EntityFrameworkCore;
using System.Data;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Employees;
using IDAProject.Web.Models.Dto.Contacts;

namespace IDAProject.Web.Api.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IDAProjectContext _dbContext;

        public EmployeesRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var searchParams = new SearchEmployeesParams
            {
                Id = id
            };

            var result = await SearchEmployeesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<int> SaveEmployeeAsync(SaveEmployeeRequestModel requestModel)
        {
            Employee? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.Employees.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);
            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveEmployeeRequestModel, Employee>(requestModel);
                _dbContext.Employees.Add(dbRecord!);
            }

            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }
        public async Task DeleteEmployeeAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.Employees.SingleAsync(x => x.Id == id);

            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.Employees.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EmployeeDto>> SearchEmployeesAsync(SearchEmployeesParams searchParams)
        {
            var result = new List<EmployeeDto>();
            IQueryable<Employee> query = _dbContext.Employees.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id);

            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.JobTypes.Any())
                {
                    query = query.Where(x => searchParams.JobTypes.Contains(x.JobTypeId.Value));
                }
                if (searchParams.JobTypeId.HasValue && searchParams.JobTypeId > 0)
                {
                    query = query.Where(x => x.JobTypeId == searchParams.JobTypeId);
                }
                if (searchParams.CommpanyId.HasValue)
                {
                    query = query.Where(x => x.CompanyId == searchParams.CommpanyId.Value);
                }

				if (searchParams.OrgUnitId.HasValue)
				{
					query = query.Where(x => x.OrgUnitId == searchParams.OrgUnitId);
				}

				if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    var words = searchParams.Keyword.Split(" ", StringSplitOptions.None | StringSplitOptions.RemoveEmptyEntries);
                    if (words.Any())
                    {
                        if (words.Length == 1)
                        {
                            var pattern = $"{words[0]}%";
                            query = query.Where(x =>
                                EF.Functions.Like(x.Name, pattern) ||
                                EF.Functions.Like(x.MiddleName!, pattern) ||
                                EF.Functions.Like(x.Surname, pattern));
                        }
                        else if (words.Length == 2)
                        {
                            query = query.Where(x => x.Name == words[0] || x.Surname == words[1]);
                            if (!query.Any())
                            {
                                query = query.Where(x => words.Contains(x.Name) || words.Contains(x.MiddleName) || words.Contains(x.Surname));
                            }
                        }
                        else if (words.Length == 3)
                        {
                            query = query.Where(x => x.Name == words[0] || x.MiddleName == words[1] || x.Surname == words[2]);
                        }
                        else
                        {
                            query = query.Where(x => words.Contains(x.Name) || words.Contains(x.MiddleName) || words.Contains(x.Surname));
                        }
                    }
                }

                if (searchParams.Blocked.HasValue)
                {
                    query = query.Where(x => x.Blocked == searchParams.Blocked.Value);
                }
            }

            result = await (from emp in query
                            select new EmployeeDto
                            {
                                Id = emp.Id,
                                Code = emp.Code,
                                Name = emp.Name,
                                MiddleName = emp.MiddleName,
                                Surname = emp.Surname,
                                BirthDate = emp.BirthDate,
                                Blocked = !emp.Blocked,
                                CellPhoneNumber = emp.CellPhoneNumber,
                                Email = emp.Email,
                                FederalNumber = emp.FederalNumber,
                                InsuranceNumber = emp.InsuranceNumber,
                                HousePhoneNumber = emp.HousePhoneNumber,
                                Partner = emp.Partner!.Name,
                                PassportId = emp.PassportId,
                                PersonalId = emp.PersonalId,
                                JobType = emp.JobType!.Name,
                                OrgUnit = emp.OrgUnit!.Name,
                                Company = emp.Company.Name,
                                State = emp.State!.Name,
                                StateShort = emp.State!.ShortName,
                                City = emp.City!.Name,
                                ShoeSize = emp.ShoeSize,
                                StateId = emp.StateId,
                                SuiteSize = emp.SuiteSize,
                                Address = emp.Address,
                                BankAccount = emp.BankAccount,
                                BankAccountAddition = emp.BankAccountAddition,
                                RoutingNumber = emp.RoutingNumber,
                                AccountingCode = emp.AccountingCode,
                                BirthPlace = emp.BirthPlace,
                                Citizenship = emp.Citizenship,
                                CityId = emp.CityId,
                                CompanyId = emp.CompanyId.Value,
                                JobTypeId = emp.JobTypeId.Value,
                                NoticeTypeId = emp.NoticeTypeId,
                                OrgUnitId = emp.OrgUnitId,
                                ZipCodeId = emp.ZipCodeId,
                                PartnerId = emp.PartnerId,
                                OwnPartnerCompany = emp.OwnPartnerCompany.Value,
                                NoticeType = emp.NoticeType!.Name,
                                //GenderId = emp.GenderId,
                                //Gender = emp.Gender!.Name,
                                ZipCode = emp.ZipCode!.ZipCode1,
                                EmployeeNumber = emp.EmployeeNumber,
                                Photo = emp.Photo

                            }).ToListAsync();

            return result;
        }
        public async Task<EmployeeSearchResponseModel?> GetEmailPhoneByEmployeeIdAsync(int id)
        {
            var result = new EmployeeSearchResponseModel();
            IQueryable<Employee> query = _dbContext.Employees;

            query = query.Where(x => x.Id == id);
            result = await (from emp in query
                            select new EmployeeSearchResponseModel
                            {
                                Phone = emp.CellPhoneNumber == null ? String.Empty : emp.CellPhoneNumber,
                                Email = emp.Email
                            }).FirstOrDefaultAsync();

            return result;
        }

		public async Task<EmployeeDto> GetNextEmployeeAsync(int currentId)
		{
			var nextEmployee = await _dbContext.Employees
			.Where(o => o.Id > currentId)
			.OrderBy(o => o.Id)
			.FirstOrDefaultAsync();

			if (nextEmployee == null)
			{
				return null;
			}
			var nextEmployeeDto = new EmployeeDto
			{
		        Id = nextEmployee.Id
			};

			return nextEmployeeDto;
		}

		public async Task<EmployeeDto> GetPreviousEmployeeAsync(int currentId)
		{
			var previousEmployee = await _dbContext.Employees
			.Where(o => o.Id < currentId)
			.OrderByDescending(o => o.Id)
			.FirstOrDefaultAsync();

			if (previousEmployee == null)
			{
				return null;
			}

			var previousEmployeeDto = new EmployeeDto
			{
				Id = previousEmployee.Id

			};

			return previousEmployeeDto;
		}

	}
}