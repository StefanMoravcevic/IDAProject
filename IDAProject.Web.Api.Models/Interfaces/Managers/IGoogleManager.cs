using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Models.Dto.Employees;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IGoogleManager
    {
        public string GetOAuthUrl(string redirectUri, int employeeId);
        public Task<EmployeeDto> HandleOAuthCallbackAsync(string code, string state, string redirectUri);
        public Task SyncFutureEventsForEmployeeAsync(int employeeId);
        public Task SyncFutureEventsForAllEmployeesAsync();
    }
}
