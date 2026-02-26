using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Users
{
    public class UserViewModel : NavigationBaseViewModel
    {
        public UserViewModel()
        {
            Employees = new List<GenericSelectOption>();
            Partners = new List<GenericSelectOption>();
            Roles = new List<GenericSelectOption>();
            UserOrgUnits = new List<GenericSelectOption>();
            UserRoles = new List<GenericSelectOption>();
            Cultures = new List<GenericSelectOption>();
            OrgUnits = new List<GenericSelectOption>();
            Printers = new List<GenericSelectOption>();
            UserData = new UserDto();
        }

        public IEnumerable<ISelectOption> Employees { get; set; }
        public IEnumerable<ISelectOption> Partners { get; set; }
        public IEnumerable<ISelectOption> Roles { get; set; }
        public IEnumerable<ISelectOption> UserRoles { get; set; }
        public IEnumerable<ISelectOption> Cultures { get; set; }
        public IEnumerable<ISelectOption> OrgUnits { get; set; }
        public IEnumerable<ISelectOption> UserOrgUnits { get; set; }
        public IEnumerable<ISelectOption> Printers { get; set; }
        public UserDto UserData { get; set; }
    }
}
