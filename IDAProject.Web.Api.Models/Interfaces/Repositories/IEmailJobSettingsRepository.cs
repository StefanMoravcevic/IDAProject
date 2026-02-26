using IDAProject.Web.Models.Dto.EmailJobSettings;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmailJobSettings;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IEmailJobSettingsRepository
    {
        Task<List<EmailJobSettingDto>> GetEmailJobSettingsAsync(EmailJobTypes emailJobType);

        Task<EmailJobSettingDto> GetEmailJobSettingsByIdAsync(int id);
        Task<List<EmailJobSettingDto>> SearchEmailJobSettingsAsync(SearchEmailJobSettingsParams searchParams);
        Task<int> SaveEmailJobSettingsAsync(EmailJobSettingDto requestModel);
        Task DeleteEmailJobSettingAsync(int id, int? userId);
    }
}
