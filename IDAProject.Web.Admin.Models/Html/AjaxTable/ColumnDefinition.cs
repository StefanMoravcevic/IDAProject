namespace IDAProject.Web.Admin.Models.Html.AjaxTable
{
    public class ColumnDefinition
    {
        public ColumnDefinition() : this(string.Empty, string.Empty)
        {
        }

        public ColumnDefinition(string name) : this(name, name)
        {
        }

        public ColumnDefinition(string name, string headerText)
        {
            Name = name;
            DataFieldName = string.Empty;
            HeaderText = headerText;
            HeaderStyle = string.Empty;
            CellStyle = string.Empty;
            IsHidden = false;
            IsOptionsColumn = false;
            Options = new List<ColumnOption>();
            OnPrepareCellContent = string.Empty;
        }

        public ColumnDefinition(string name, string dataFieldName, string headerText)
        {
            Name = name;
            DataFieldName = dataFieldName;
            HeaderText = headerText;
            HeaderStyle = string.Empty;
            CellStyle = string.Empty;
            IsHidden = false;
            IsOptionsColumn = false;
            Options = new List<ColumnOption>();
            OnPrepareCellContent = string.Empty;
        }

        public string Name { get; set; }
        public string DataFieldName { get; set; }
        public string HeaderText { get; set; }
        public string HeaderStyle { get; set; }
        public string CellStyle { get; set; }
        public bool IsHidden { get; set; }
        public bool IsOptionsColumn { get; set; }
        public List<ColumnOption> Options { get; set; }
        public string OnPrepareCellContent { get; set; }
    }
}