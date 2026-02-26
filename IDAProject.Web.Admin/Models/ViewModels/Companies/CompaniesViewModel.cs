using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.Companies
{
    public class CompaniesViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public CompaniesViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Name", _localizer["Company name"]),
                new ColumnDefinition("ResponsiblePerson", _localizer["Responsible person"]),
                new ColumnDefinition("WebAddress", _localizer["Web address"]),
                new ColumnDefinition("Email", _localizer["E-mail"]),
                new ColumnDefinition("Phone", _localizer["Phone"]),
                new ColumnDefinition("Fax", _localizer["PIB"])
            };
        }
    }
}
