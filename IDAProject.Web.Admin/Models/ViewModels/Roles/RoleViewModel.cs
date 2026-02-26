using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Roles
{
    public class RoleViewModel : NavigationBaseViewModel
    {
        public RoleViewModel()
        {
            Role = new RoleDto();
            Features = new List<GenericSelectOption>();
            RoleFeatures = new List<RoleFeatureDto>();
            Companies = new List<GenericSelectOption>();
        }

        public RoleDto Role { get; set; }
        public IEnumerable<ISelectOption> Features { get; set; }
        public IEnumerable<ISelectOption> Companies { get; set; }
        public List<RoleFeatureDto> RoleFeatures { get; set; }

        //public int ReadOnly { get; set; }
    }
}
