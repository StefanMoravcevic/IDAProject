using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.TagHelpers
{
    public class SelectViewModel : BaseTagHelperViewModel
    {
        private string _id;
        private string _name;
        private string _label;
        private IEnumerable<ISelectOption> _options;
        private int? _selectedValue;
        private string _disabledAttribute;
		private string _tag;
        private string _additionalAttributes;

        public SelectViewModel()
        {
            _id = string.Empty;
            _name = string.Empty;
            _label = string.Empty;
            _selectedValue = new int?();
            _options = new List<ISelectOption>();
            _disabledAttribute = string.Empty;
            _tag = string.Empty;
            _additionalAttributes = string.Empty;
        }

        public IEnumerable<ISelectOption> Options
        {
            get { return _options; }
            set { _options = value; }
        }

        public int? SelectedValue
        {
            get { return _selectedValue; }
            set { _selectedValue = value; }
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

        public string DisabledAttribute
        {
            get { return _disabledAttribute; }
            set { _disabledAttribute = value; }
        }

        public string AdditionalAttributes
        {
            get { return _additionalAttributes; }
            set { _additionalAttributes = value; }
        }

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
    }
}
