namespace IDAProject.Web.Admin.Models.ViewModels
{
    public class PageViewModel<T> : NavigationBaseViewModel
    {
        private T? _data;

        public PageViewModel() : base()
        {
        }

        public T? Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
