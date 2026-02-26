using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Models.TagHelpers
{
    public class SelectGroupedViewModel : BaseTagHelperViewModel
    {
        private string _id;
        private string _name;
        private string _label;
        private IEnumerable<SelectGroupOption> _options;
        private int? _selectedValue;
        private string _emptyOptionText;
        private string _emptyOptionValue;

        public SelectGroupedViewModel()
        {
            _options = new List<SelectGroupOption>();
            _emptyOptionValue = string.Empty;
            _id = string.Empty;
            _name = string.Empty;
            _label = string.Empty;
            _emptyOptionText = string.Empty;
        }

        public IEnumerable<SelectGroupOption> Options
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
    }
}
