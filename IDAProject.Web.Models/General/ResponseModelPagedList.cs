namespace IDAProject.Web.Models.General
{
    public class ResponseModelPagedList<T> : ResponseModelBase
    {
        private List<T> _payload;

        public ResponseModelPagedList() : base()
        {
            _payload = new List<T>();
        }

        public List<T> Payload
        {
            get { return _payload; }
            set { _payload = value; }
        }
        public int TotalRowCount { get; set; }
    }
}