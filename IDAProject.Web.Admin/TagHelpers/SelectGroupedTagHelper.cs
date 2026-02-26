using Microsoft.AspNetCore.Mvc.Rendering;
using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.TagHelpers.Base;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.TagHelpers
{
    [HtmlTargetElement("tms-select-grouped", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SelectGroupedTagHelper : BaseTagHelper
    {
        private string _id;
        private string _name;
        private string _label;
        private IEnumerable<SelectGroupOption> _options;
        private int? _selectedOption;
        private string _emptyOptionText;
        private string _emptyOptionValue;

        public SelectGroupedTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            _options = new List<SelectGroupOption>();
            _id = string.Empty;
            _name = string.Empty;
            _emptyOptionText = string.Empty;
            _emptyOptionValue = string.Empty;
            _label = string.Empty;
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public IEnumerable<SelectGroupOption> Options
        {
            get { return _options; }
            set { _options = value; }
        }

        public int? SelectedOption
        {
            get { return _selectedOption; }
            set { _selectedOption = value; }
        }


        public string EmptyOptionText
        {
            get { return _emptyOptionText; }
            set { _emptyOptionText = value; }
        }

        public string EmptyOptionValue
        {
            get { return _emptyOptionValue; }
            set { _emptyOptionValue = value; }
        }

        protected override BaseTagHelperViewModel GetViewModel()
        {
            var viewModel = new SelectGroupedViewModel
            {
                Id = _id,
                Name = _name,
                Options = _options,
                EmptyOptionText = _emptyOptionText,
                EmptyOptionValue = _emptyOptionValue,
                SelectedValue = _selectedOption,
                Label = _label
            };

            return viewModel;
        }
    }
}
