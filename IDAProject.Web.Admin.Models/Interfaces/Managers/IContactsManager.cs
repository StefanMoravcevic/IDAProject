using IDAProject.Web.Models.Dto.Contacts;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.Contacts;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IContactsManager
    {
        Task<ResponseModelList<ContactDto>> SearchContactsAsync(SearchContactsParams searchParams);

        Task<ResponseModel<ContactDto>> GetContactByIdAsync(int id);

        Task<ResponseModel<int>> SaveContactAsync(SaveContactRequestModel requestModel);

        Task<ResponseModelBase> DeleteContactAsync(int id, int userId);

        Task<IEnumerable<ISelectOption>> GetContactsAsSelectOptionsAsync(int? companyId, int? contactCompanyId, int? partnerId, bool? isCompany);
    }
}
