namespace IDAProject.Web.Admin.Models.Html.AjaxTable
{
    public class ColumnOption
    {
        public ColumnOption(string name)
        {
            Name = name;
            FontIconClass = string.Empty;
            Color = string.Empty;
        }

        public string Name { get; }
        public string FontIconClass { get; set; }
        public string Color { get; set; }
    }
}
