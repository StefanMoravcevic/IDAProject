using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Admin.Models.Interfaces.ViewModel;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Admin.Models.ViewModels;
using IDAProject.Web.Admin.TagHelpers.Base;

namespace IDAProject.Web.Admin.TagHelpers
{
    [HtmlTargetElement("tms-ajax-table", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class AjaxTableTagHelper : BaseTagHelper
    {
        private string _id;
        private bool _showOptions;
        private IAjaxTableViewModel _viewModel;
        private string _tableSettingsReferenceTable;

        public AjaxTableTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            _id = string.Empty;
            _showOptions = true;
            _viewModel = new NavigationWithAjaxTableViewModel();
            _tableSettingsReferenceTable = string.Empty;
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool ShowOptions
        {
            get { return _showOptions; }
            set { _showOptions = value; }
        }

        public string TableSettingsReferenceTable
        {
            get { return _tableSettingsReferenceTable; }
            set { _tableSettingsReferenceTable = value; }
        }

        public IAjaxTableViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        private void ComputeAjaxTableSettings()
        {
            var ts = _viewModel.TableSettings;
            foreach (var column in _viewModel.Columns)
            {
                column.IsHidden = ts.HiddenColumns.Contains(column.Name);
            }

            if (ts.CustomColumnsOrder.Any())
            {
                var newColumnsOrder = new List<ColumnDefinition>();

                foreach (var columnName in ts.CustomColumnsOrder)
                {
                    var cd = _viewModel.Columns.FirstOrDefault(x => string.Equals(x.Name, columnName, StringComparison.OrdinalIgnoreCase));
                    if (cd != null)
                    {
                        newColumnsOrder.Add(cd);
                    }
                }
                _viewModel.Columns = newColumnsOrder;
            }
        }

        protected override BaseTagHelperViewModel GetViewModel()
        {
            ComputeAjaxTableSettings();

            var model = new AjaxTableViewModel
            {
                Id = _id,
                TableSettingsReferenceTable = _tableSettingsReferenceTable,
                TableWrapperId = $"{_id}_wrapper",
                TableDefinition = _viewModel,
                ShowOptions = _showOptions
            };

            return model;
        }
    }
}