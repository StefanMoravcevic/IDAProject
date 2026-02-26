using IDAProject.Web.Admin.Models.Interfaces.ViewModel;
using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Admin.Models.ViewModels;

namespace IDAProject.Web.Admin.Models.TagHelpers
{
    public class AjaxTableViewModel : BaseTagHelperViewModel
    {
        public AjaxTableViewModel()
        {
            Id = string.Empty;
            TableWrapperId = string.Empty;
            TableDefinition = new NavigationWithAjaxTableViewModel();
            TableSettingsReferenceTable = string.Empty;
            ShowOptions = true;
        }

        public string Id { get; internal set; }

        public string TableSettingsReferenceTable { get; internal set; }

        public string TableWrapperId { get; internal set; }

        public IAjaxTableViewModel TableDefinition { get; internal set; }

        public bool ShowOptions { get; set; }
    }
}
