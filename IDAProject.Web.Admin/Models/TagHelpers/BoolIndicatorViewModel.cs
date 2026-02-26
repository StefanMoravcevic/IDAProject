using IDAProject.Web.Admin.Models.TagHelpers.Base;

namespace IDAProject.Web.Admin.Models.TagHelpers
{
    public class BoolIndicatorViewModel : BaseTagHelperViewModel
    {
        bool? _value;

        public BoolIndicatorViewModel()
        {
            _value = new bool?();
        }

        public bool? Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
