
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.TagHelpers.Base;
using IDAProject.Web.Admin.TagHelpers.Base;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.TagHelpers
{
    [HtmlTargetElement("tms-input", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class InputTagHelper : BaseTagHelper
    {
        private string _id;
        private string _name;
        private string _label;
        private object _value;
        private string _placeholder;
        private FromInputType _type;
        private bool _checked;
        private InputFileAccept _accept;
        private string _classes;
        private ModelExpression? _field;

        private bool _required;
        private int _decimals;
        private string _tag;

        public InputTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            _id = string.Empty;
            _name = string.Empty;
            _label = string.Empty;
            _value = string.Empty;
            _placeholder = string.Empty;
            Type = FromInputType.Text;
            _checked = false;
            _accept = InputFileAccept.JpgPng;
            _required = false;
            _classes = string.Empty;
            _decimals = 0;
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

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public FromInputType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        public InputFileAccept Accept
        {
            get { return _accept; }
            set { _accept = value; }
        }

        public ModelExpression Field
        {
            get { return _field!; }
            set { _field = value; }
        }

        public string Classes
        {
            get { return _classes; }
            set { _classes = value; }
        }

        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }

        public int Decimals
        {
            get { return _decimals; }
            set { _decimals = value; }
        }

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        protected override BaseTagHelperViewModel GetViewModel()
        {
            var viewModel = new InputViewModel
            {
                Id = _id,
                Label = _label,
                Type = _type,
                Checked = _checked,
                Placeholder = _placeholder,
                Classes = _classes,
                Value = DataHelpers.SafeString(_value),
                AdditionalAttributes = BuildAdditionalAttributes(),
                Name = _field == null ? _name : _field.Name,
                Tag = _tag
            };

            if (_type == FromInputType.Date)
            {
                var val = new DateTime?();
                if (_value is DateTime?)
                {
                    val = _value as DateTime?;
                }
                if (_value is DateTime)
                {
                    val = (DateTime)_value;
                }


                if(val.HasValue && val.Value == DateTime.MinValue)
                {
                    viewModel.Value = string.Empty;
                }
                else
                {
                    viewModel.Value = DisplayFormatHelpers.FormatDate(val);
                }
                
            }
            if(_type == FromInputType.Number)
            {
                viewModel.Value = DataHelpers.GetFormatedNumericValueForInput(_value, _decimals);
            }
            else if(Type == FromInputType.File)
            {
                switch(Accept)
                {
                    case InputFileAccept.Png:
                        {
                            viewModel.Accept = $".png,{Constants.ContentType_Png}";
                            break;
                        }
                    case InputFileAccept.Jpg:
                        {
                            viewModel.Accept = $".jpg,{Constants.ContentType_Jpg}";
                            break;
                        }
                    case InputFileAccept.JpgPng:
                        {
                            viewModel.Accept = $"{Constants.ContentType_Jpg},{Constants.ContentType_Png}";
                            break;
                        }
                    case InputFileAccept.Pdf:
                        {
                            viewModel.Accept = $".pdf,{Constants.ContentType_Pdf}";
                            break;
                        }
                    case InputFileAccept.Xlsx:
                        {
                            viewModel.Accept = $".xlsx,{Constants.ContentType_Xlsx}";
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException($"Invalit accept method: [{Accept}] for input type file.");
                        }
                }
            }

            if(string.IsNullOrEmpty(viewModel.Id))
            {
                viewModel.Id = DataHelpers.GetRandomHtmlId();
            }

            return viewModel;
        }

        private string BuildAdditionalAttributes()
        {
            var result = string.Empty;
            if (Required)
            {
                result = "required";
            }
            return result;
        }
    }
}
