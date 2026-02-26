namespace IDAProject.Web.Models.General
{
    public class ResponseModelList<T> : ResponseModelBase
    {
        private List<T> _payload;

        public ResponseModelList() : base()
        {
            _payload = new List<T>();
        }

        public List<T> Payload
        {
            get { return _payload; }
            set { _payload = value; }
        }
    }
}