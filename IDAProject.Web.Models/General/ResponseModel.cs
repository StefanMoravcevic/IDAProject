namespace IDAProject.Web.Models.General
{
    public class ResponseModel<T> : ResponseModelBase
    {
        private T? _payload;

        public ResponseModel() : base()
        {
            _payload = default(T);
        }

        public T? Payload
        {
            get { return _payload; }
            set { _payload = value; }
        }
    }
}
