using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Companies;

namespace IDAProject.Web.Api.Managers
{
    public class CompaniesManager : ICompaniesManager
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly ILogger _logger;

        public CompaniesManager(ILogger<CompaniesManager> logger, ICompaniesRepository companiesRepository)
        {
            _logger = logger;
            _companiesRepository = companiesRepository;
        }


        public async Task<ResponseModelList<CompanyDto>> SearchCompaniesAsync(SearchCompaniesParams searchParams)
        {
            var result = new ResponseModelList<CompanyDto>();
            try
            {
                result.Payload = await _companiesRepository.SearchCompaniesAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<CompanyDto>> GetCompanyByIdAsync(int id)
        {
            var result = new ResponseModel<CompanyDto>();
            try
            {
                result.Payload = await _companiesRepository.GetCompanyByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The company with the specified id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }
        public async Task<ResponseModelBase> DeleteCompanyAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _companiesRepository.DeleteCompanyAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveCompanyAsync(SaveCompanyRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _companiesRepository.SaveCompanyAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        #region Org units
        public async Task<ResponseModelList<OrgUnitDto>> SearchOrgUnitsAsync(SearchOrgUnitsParams searchParams)
        {
            var result = new ResponseModelList<OrgUnitDto>();
            try
            {
                result.Payload = await _companiesRepository.SearchOrgUnitsAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<OrgUnitDto>> GetOrgUnitByIdAsync(int id)
        {
            var result = new ResponseModel<OrgUnitDto>();
            try
            {
                result.Payload = await _companiesRepository.GetOrgUnitByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The org unit with the specified id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }
        public async Task<ResponseModelBase> DeleteOrgUnitAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _companiesRepository.DeleteOrgUnitAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrgUnitAsync(SaveOrgUnitRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _companiesRepository.SaveOrgUnitAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }
        #endregion
    }
}
