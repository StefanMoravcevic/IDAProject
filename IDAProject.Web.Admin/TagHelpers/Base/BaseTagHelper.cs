using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers.Base;

namespace IDAProject.Web.Admin.TagHelpers.Base
{
    public abstract class BaseTagHelper : TagHelper
    {
        private readonly IHtmlHelper _html;

        public BaseTagHelper(IHtmlHelper htmlHelper)
        {
            _html = htmlHelper;
        }


        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        protected abstract BaseTagHelperViewModel GetViewModel();


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var viewModel = GetViewModel();

            // render only if we have viewModel (e.g. notofications)
            if (viewModel != null)
            {
                //Contextualize the html helper
                var htmlContextAware = _html as IViewContextAware;
                htmlContextAware!.Contextualize(ViewContext);

                var childContent = await output.GetChildContentAsync();
                if (childContent != null)
                {
                    viewModel.ChildContent = childContent.GetContent();
                }
                var partialViewName = GetType().Name.Replace("TagHelper", ".cshtml");
                var content = await _html.PartialAsync($"~/TagHelpers/{partialViewName}", viewModel);

                // disable outer tag rendering
                output.TagName = null;
                output.TagMode = TagMode.StartTagAndEndTag;

                // render inner content
                output.Content.SetHtmlContent(content);
            }
        }
    }
}