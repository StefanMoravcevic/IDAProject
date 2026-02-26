namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface ISecurityRepository
    {
        Task<bool> ValidateApiKeyAsync(string apiKey);

        Task<string> GetEmployeeCompanyNameAsync(int idEmployee);

        Task<int?> GetEmployeeCompanyIdAsync(int idEmployee);
    }
}
