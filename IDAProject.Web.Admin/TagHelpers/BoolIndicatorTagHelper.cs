using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Admin.TagHelpers.Base;

namespace IDAProject.Web.Admin.TagHelpers
{
    [HtmlTargetElement("tms-indicator-bool", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class BoolIndicatorTagHelper : BaseTagHelper
    {
        bool? _value;

        public BoolIndicatorTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            _value = new bool?();
        }

        public bool? Value
        {
            get { return _value; }
            set { _value = value; }
        }

        protected override BaseTagHelperViewModel GetViewModel()
        {
            var viewModel = new BoolIndicatorViewModel
            {
                Value = _value
            };
            return viewModel;
        }
    }
}
