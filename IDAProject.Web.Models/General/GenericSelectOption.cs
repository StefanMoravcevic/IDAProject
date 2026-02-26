using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Models.General
{
    public class GenericSelectOption : ISelectOption
    {
        private int? _id;
        private string _value;

        public GenericSelectOption()
        {
            _id = new int?();
            _value = string.Empty;
        }

        public GenericSelectOption(int id, string value)
        {
            _id = id;
            _value = value;
        }

        public GenericSelectOption(int id) : this(id, string.Empty)
        {
        }
        
        public int? Value
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Description
        {
            get { return _value; }
            set { _value = value; }
        }

        public override string ToString()
        {
            return _id + " -> " + _value;
        }
    }
}
