using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Messages;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface INotificationsManager
    {
        Task SendQueuedEmailsAsync();
        Task<ResponseModelList<EmailDto>> SearchEmailsAsync(SearchEmailsParams searchParams);
        Task<ResponseModelBase> SendNewAccountRequest(RegisterModel requestModel);
        Task<ResponseModelBase> SendResetPasswordRequest(RegisterModel requestModel);
        Task<ResponseModelBase> SendAdminResetPassword(string pass, string userName, string mailTo, string textTitle);

    }
}
