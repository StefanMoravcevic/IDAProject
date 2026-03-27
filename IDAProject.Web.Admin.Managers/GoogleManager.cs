using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IDAProject.Web.Admin.Managers
{
    public class GoogleManager : BaseManager, IGoogleManager
    {
        public GoogleManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<GoogleManager> logger)
    : base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModel<string>> GetOAuthUrl(int employeeId)
        {
            var result = await GetAsync<ResponseModel<string>>($"api/google/getOAuthUrl/{employeeId}");
            return result;
        }

        public async Task<EmployeeDto> HandleOAuthCallbackAsync(string code, string state, string redirectUri)
        {
            var result = await GetAsync<EmployeeDto>($"api/google/handleOAuthCallbackAsync/{code}/{state}/{redirectUri}");
            return result;
        }
    }
}
