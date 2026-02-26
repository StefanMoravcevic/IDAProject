using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Admin.TagHelpers.Base;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.TagHelpers
{
    [HtmlTargetElement("tms-select", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SelectTagHelper : BaseTagHelper
    {
        private string _id;
        private string _name;
        private string _label;
        private IEnumerable<ISelectOption> _options;
        private int? _selectedOption;
        private string _emptyOptionText;
        private bool _disabled;
		private string _tag;
        private bool _required;

        public SelectTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            _options = new List<ISelectOption>();
            _selectedOption = null;
            _id = string.Empty;
            _name = string.Empty;
            _label = string.Empty;
            _emptyOptionText = string.Empty;
            _disabled = false;
            _tag = string.Empty;
            _required = false;
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

        public bool Disabled
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        public IEnumerable<ISelectOption> Options
        {
            get { return _options; }
            set { _options = value; }
        }

        public int? SelectedOption
        {
            get { return _selectedOption!; }
            set { _selectedOption = value; }
        }


        public string EmptyOptionText
        {
            get { return _emptyOptionText; }
            set { _emptyOptionText = value; }
        }

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }

        protected override BaseTagHelperViewModel GetViewModel()
        {
            var viewModel = new SelectViewModel
            {
                Id = _id,
                Name = _name,
                Label = _label,
                Options = _options,
                SelectedValue = _selectedOption,
                DisabledAttribute = _disabled ? "disabled='true'" : string.Empty,
                AdditionalAttributes = BuildAdditionalAttributes(),
                Tag = _tag,
            };

            if(!string.IsNullOrEmpty(_emptyOptionText))
            {
                var newList = new List<ISelectOption>();

                var emptyOption = new GenericSelectOption
                {
                    Description = _emptyOptionText
                };

                newList.Add(emptyOption);

                foreach (var option in viewModel.Options)
                {
                    newList.Add(option);
                }
                viewModel.Options = newList;
            }

            return viewModel;
        }

        private string BuildAdditionalAttributes()
        {
            var result = string.Empty;
            if (Required)
            {
                result = "required";
            }
            return result;
        }
    }
}
