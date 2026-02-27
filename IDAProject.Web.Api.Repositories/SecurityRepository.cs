using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;

namespace IDAProject.Web.Api.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly IdaContext _dbContext;

        public SecurityRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetEmployeeCompanyNameAsync(int idEmployee)
        {
            var result = string.Empty;

            // first check if employee is internal
            var companyId = await GetEmployeeCompanyIdAsync(idEmployee);


            if (companyId.HasValue)
            {
                result = await (from c in _dbContext.Companies where c.Id == companyId.Value select c.Name).SingleAsync();
            }
            else
            {

                var partner = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == idEmployee);
                if(partner != null)
                {
                    result = partner.Name;
                }
            }
            return result;
        }

        public async Task<int?> GetEmployeeCompanyIdAsync(int idEmployee)
        {
            var companyId = await (from emp in _dbContext.Employees
                                     where emp.Id == idEmployee
                                     select emp.CompanyId).FirstOrDefaultAsync();
            return companyId;
        }

        public async Task<bool> ValidateApiKeyAsync(string apiKey)
        {
            var isValid = false;

            if (Guid.TryParse(apiKey, out Guid key))
            {
                isValid = await _dbContext.Integrations.AnyAsync(x => x.ApiKey == key && x.IsActive == true);
            }
            return isValid;
        }
    }
}