using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Partners
{
    public class PartnerViewModel : NavigationBaseViewModel
    {
        public PartnerViewModel()
        {
            Partner = new PartnerDto();
            PartnerTypes = new List<GenericSelectOption>();
            PartnerCategories = new List<GenericSelectOption>();
            States = new List<GenericSelectOption>();
            PaymentConditions = new List<GenericSelectOption>();
            PrimaryContacts = new List<GenericSelectOption>();
            IncomeTypes = new List<GenericSelectOption>();
            Cities = new List<GenericSelectOption>();
            ContactCompanies = new List<GenericSelectOption>();
        }

        public PartnerDto Partner { get; set; }

        public IEnumerable<ISelectOption> PartnerTypes { get; set; }
        public IEnumerable<ISelectOption> PartnerCategories { get; set; }
        public IEnumerable<ISelectOption> PaymentConditions { get; set; }
        public IEnumerable<ISelectOption> PrimaryContacts { get; set; }
        public IEnumerable<ISelectOption> IncomeTypes { get; set; }
        public IEnumerable<ISelectOption> Cities { get; set; }
        public IEnumerable<ISelectOption> States { get; set; }
        public IEnumerable<ISelectOption> ContactCompanies { get; set; }
        public int ReadOnly { get; set; }
    }
}
