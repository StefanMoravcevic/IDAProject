namespace IDAProject.Web.Admin.Models.TagHelpers.Base
{
    public class BaseTagHelperViewModel
    {
        private string _childContent;

        public BaseTagHelperViewModel()
        {
            _childContent = string.Empty;
        }

        public string ChildContent
        {
            get { return _childContent; }
            set { _childContent = value; }
        }
    }
}
