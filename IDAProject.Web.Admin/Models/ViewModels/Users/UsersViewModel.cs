using IDAProject.Web.Models.Dto.Common;
//using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.Users;

namespace IDAProject.Web.Admin.Models.ViewModels.Users
{
    public class UsersViewModel : NavigationBaseViewModel
    {
        public UsersViewModel()
        {
            Users = new List<UserTableItemDto>();
            //TableSettings = new UserTableSettings();
            AllColumns = new List<string>
            {
                "Id",
                "Korisničko ime",
                "Email",
                "Broj telefona",
                "Aktivan",
                "Radnik",
                "Jezik",
                //"PartnerName",
                "Role"
                //"NumberOfRoles"
            };
        }

        public List<UserTableItemDto> Users { get; internal set; }
        //public UserTableSettings TableSettings { get; internal set; }

        public List<string> AllColumns { get; }
    }
}
