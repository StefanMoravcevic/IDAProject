using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Admin.TagHelpers.Base;

namespace IDAProject.Web.Admin.TagHelpers
{
    [HtmlTargetElement("tms-modal", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ModalTagHelper : BaseTagHelper
    {
        private string _id;
        private string _title;
        private bool _showSaveButton;
        private string _saveButtonText;
        private ModalSize _modalSize;

        public ModalTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            _id = string.Empty;
            _title = string.Empty;
            _showSaveButton = true;
            _saveButtonText = "Save";
            _modalSize = ModalSize.Default;
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

        public bool SaveButton
        {
            get { return _showSaveButton; }
            set { _showSaveButton = value; }
        }

        public string SaveButtonText
        {
            get { return _saveButtonText; }
            set { _saveButtonText = value; }
        }

        public ModalSize ModalSize
        {
            get { return _modalSize; }
            set { _modalSize = value; }
        }

        protected override BaseTagHelperViewModel GetViewModel()
        {
            var model = new ModalViewModel
            {
                Id = _id,
                Title = _title,
                ShowConfirmButton = SaveButton,
                ConfirmButtonText = SaveButtonText
            };

            switch(_modalSize)
            {
                case ModalSize.Small:
                    {
                        model.ModalSizeClass = "modal-sm";
                        break;
                    }
                case ModalSize.Large:
                    {
                        model.ModalSizeClass = "modal-lg";
                        break;
                    }
                case ModalSize.ExtraLarge:
                    {
                        model.ModalSizeClass = "modal-xl";
                        break;
                    }
                default:
                    {
                        model.ModalSizeClass = string.Empty;
                        break;
                    }
            }

            model.IdTitle = $"{_id}-title";
            model.IdContent = $"{_id}-content";
            model.IdConfirm = $"{_id}-confirm";
            return model;
        }
    }
}