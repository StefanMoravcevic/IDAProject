using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Admin.TagHelpers.Base;

namespace IDAProject.Web.Admin.TagHelpers
{
    [HtmlTargetElement("tms-notification", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class NotificationTagHelper : BaseTagHelper
    {
        private NotificationViewModel? _model;

        public NotificationTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            _model = null;
        }

        public NotificationViewModel Model
        {
            get { return _model!; }
            set { _model = value; }
        }

        protected override BaseTagHelperViewModel GetViewModel()
        {
            return _model!;
        }
    }
}
