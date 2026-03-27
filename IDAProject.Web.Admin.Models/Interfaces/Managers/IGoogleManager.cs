using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IGoogleManager
    {
        public Task<ResponseModel<string>> GetOAuthUrl(int employeeId);
        public Task<EmployeeDto> HandleOAuthCallbackAsync(string code, string state, string redirectUri);
    }
}
