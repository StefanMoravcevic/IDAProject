using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Companies
{
    public class OrgUnitViewModel : NavigationBaseViewModel
    {
        public OrgUnitViewModel()
        {
            OrgUnit = new OrgUnitDto();
            Companies = new List<GenericSelectOption>();
            OrgUnits = new List<GenericSelectOption>();
        }

        public OrgUnitDto OrgUnit { get; set; }

        public IEnumerable<ISelectOption> Companies { get; set; }
        public IEnumerable<ISelectOption> OrgUnits { get; set; }
        public int ReadOnly { get; set; }
    }
}
