using Microsoft.Extensions.Localization;
using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.Addresses
{
    public class AddressesViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public AddressesViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>()
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("StreetName", _localizer["Street name"]),
                new ColumnDefinition("StreetNumber", _localizer["Street no"]),
                new ColumnDefinition("State", _localizer["State"]),
                new ColumnDefinition("City", _localizer["City"]),
                new ColumnDefinition("ZipCode", _localizer["ZipCode"]),
            };
        }

        public int Id { get; set; }
        public int AddressTypeId { get; set; }

        //public int AddressId { get; set; }

    }
}
