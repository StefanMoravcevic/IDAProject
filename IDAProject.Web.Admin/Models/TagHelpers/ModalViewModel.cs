using IDAProject.Web.Admin.Models.TagHelpers.Base;

namespace IDAProject.Web.Admin.Models.TagHelpers
{
    public class ModalViewModel : BaseTagHelperViewModel
    {
        private string _id;
        private string _title;

        private string _idTitle;
        private string _idContent;
        private string _idConfirm;
        private string _modalSizeClass;

        private bool _showConfirmButton;
        private string _confirmButtonText;

        public ModalViewModel()
        {
            _id = string.Empty;
            _title = string.Empty;
            _idTitle = string.Empty;
            _idContent = string.Empty;
            _idConfirm = string.Empty;
            _showConfirmButton = false;
            _confirmButtonText = string.Empty;
            _modalSizeClass = string.Empty;
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string IdTitle
        {
            get { return _idTitle; }
            set { _idTitle = value; }
        }

        public string IdContent
        {
            get { return _idContent; }
            set { _idContent = value; }
        }

        public string ModalSizeClass
        {
            get { return _modalSizeClass; }
            set { _modalSizeClass = value; }
        }

        public string IdConfirm
        {
            get { return _idConfirm; }
            set { _idConfirm = value; }
        }

        public bool ShowConfirmButton
        {
            get { return _showConfirmButton; }
            set { _showConfirmButton = value; }
        }

        public string ConfirmButtonText
        {
            get { return _confirmButtonText; }
            set { _confirmButtonText = value; }
        }
    }
}
