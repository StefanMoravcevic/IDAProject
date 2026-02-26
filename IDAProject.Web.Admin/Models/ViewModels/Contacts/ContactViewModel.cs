using IDAProject.Web.Models.Dto.Contacts;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Contacts
{
    public class ContactViewModel : NavigationBaseViewModel
    {
        public ContactViewModel()
        {
            Contact = new ContactDto();
            States = new List<GenericSelectOption>();
            Cities = new List<GenericSelectOption>();
            Partners = new List<GenericSelectOption>();
            MethodsOfCommunication = new List<GenericSelectOption>();
            PartnerCategories = new List<GenericSelectOption>();
            Genders = new List<GenericSelectOption>();
            ContactCompanies = new List<GenericSelectOption>();

            ContactTypes = new List<GenericSelectOption>
            {
                new GenericSelectOption(1,"Company"),
                new GenericSelectOption(0,"Personal contact")
            };
        }

        public ContactDto Contact { get; set; }

        public IEnumerable<ISelectOption> Cities { get; set; }
        public IEnumerable<ISelectOption> States { get; set; }
        public IEnumerable<ISelectOption> Partners { get; set; }
        public IEnumerable<ISelectOption> MethodsOfCommunication { get; set; }
        public IEnumerable<ISelectOption> ContactTypes { get; set; }
        public IEnumerable<ISelectOption> PartnerCategories { get; set; }
        public IEnumerable<ISelectOption> Genders { get; set; }
        public IEnumerable<ISelectOption> ContactCompanies { get; set; }
        public int ReadOnly { get; set; }
    }
}
