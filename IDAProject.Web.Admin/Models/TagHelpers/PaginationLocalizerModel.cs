using IDAProject.Web.Admin.Models.TagHelpers.Base;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.TagHelpers
{
    public class PaginationLocalizerModel : BaseTagHelperViewModel
    {
        private string _showing;
        private string _first;
        private string _last;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private string _to;
        private string _of;
        private string _rows;
        private string _rows1;
        private string _rows234;
        private string _noEntries;

        public PaginationLocalizerModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            _rows = _localizer["rows"];
            _rows1 = _localizer["rows1"];
            _rows234 = _localizer["rows234"];
            _showing = _localizer["Showing"];
            _first = _localizer["First"];
            _last = _localizer["Last"];
            _to = _localizer["to"];
            _of = _localizer["of"];
            _noEntries = _localizer["No entries"];
        }

        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        public string Showing
        {
            get { return _showing; }
            set { _showing = value; }
        }

        public string First
        {
            get { return _first; }
            set { _first = value; }
        }

        public string Last
        {
            get { return _last; }
            set { _last = value; }
        }

        public string Of
        {
            get { return _of; }
            set { _of = value; }
        }

        public string Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public string Rows1
        {
            get { return _rows1; }
            set { _rows1 = value; }
        }

        public string Rows234
        {
            get { return _rows234; }
            set { _rows234 = value; }
        }

        public string NoEntries
        {
            get { return _noEntries; }
            set { _noEntries = value; }
        }
    }
}
