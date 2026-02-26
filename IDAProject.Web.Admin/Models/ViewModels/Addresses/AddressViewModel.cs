using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Addresses
{
    public class AddressViewModel : NavigationBaseViewModel
    {
        public AddressViewModel()
        {
            Address = new AddressDto();
            States = new List<GenericSelectOption>();
            Cities = new List<GenericSelectOption>();
            ZipCodes = new List<GenericSelectOption>();
        }

        public int AddressTypeId { get; set; }
        public int Id { get; set; }
        public AddressDto Address { get; set; }

        public IEnumerable<ISelectOption> States { get; set; }
        public IEnumerable<ISelectOption> Cities { get; set; }

        public IEnumerable<ISelectOption> ZipCodes { get; set; }

    }
}
