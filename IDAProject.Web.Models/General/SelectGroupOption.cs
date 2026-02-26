namespace IDAProject.Web.Models.General
{
    public class SelectGroupOption
    {
        private string _name;
        private IEnumerable<GenericSelectOption> _options;

        public SelectGroupOption() : this(string.Empty)
        {
        }

        public SelectGroupOption(string name)
        {
            _name = name;
            _options = new List<GenericSelectOption>();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public IEnumerable<GenericSelectOption> Options
        {
            get { return _options; }
            set { _options = value; }
        }
    }
}
