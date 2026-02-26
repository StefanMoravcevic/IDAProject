using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Companies
{
    public class CompanyViewModel : NavigationBaseViewModel
    {
        public CompanyViewModel()
        {
            Company = new CompanyDto();
            Companies = new List<GenericSelectOption>();
            FactoringHouses = new List<GenericSelectOption>();
            ZipCodes = new List<GenericSelectOption>();
            States = new List<GenericSelectOption>();
            Cities = new List<GenericSelectOption>();
        }

        public CompanyDto Company { get; set; }

        public IEnumerable<ISelectOption> Companies { get; set; }
        public IEnumerable<ISelectOption> FactoringHouses { get; set; }
        public IEnumerable<ISelectOption> ZipCodes { get; set; }
        public IEnumerable<ISelectOption> States { get; set; }
        public IEnumerable<ISelectOption> Cities { get; set; }
        public int ReadOnly { get; set; }
    }
}
