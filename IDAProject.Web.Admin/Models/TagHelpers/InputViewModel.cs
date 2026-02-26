using IDAProject.Web.Admin.Models.TagHelpers.Base;

namespace IDAProject.Web.Admin.Models.TagHelpers
{
    public class InputViewModel : BaseTagHelperViewModel
    {
        private string _id;
        private string _name;
        private string _label;
        private string _value;
        private string _placeholder;
        private FromInputType _type;
        private bool _checked;
        private string _accept;
        private string _additionalAttributes;
        private string _classes;
        private string _tag;

        public InputViewModel()
        {
            _id = string.Empty;
            _name = string.Empty;
            _label = string.Empty;
            _value = string.Empty;
            _placeholder = string.Empty;
            _type = FromInputType.Text;
            _checked = false;
            _accept = string.Empty;
            _additionalAttributes = string.Empty;
            _classes = string.Empty;
            _tag = string.Empty;
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

        public string Placeholder
        {
            get { return _placeholder; }
            set { _placeholder = value; }
        }

        public FromInputType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        public string Accept
        {
            get { return _accept; }
            set { _accept = value; }
        }

        public string AdditionalAttributes
        {
            get { return _additionalAttributes; }
            set { _additionalAttributes = value; }
        }

        public string Classes
        {
            get { return _classes; }
            set { _classes = value; }
        }

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
    }
}