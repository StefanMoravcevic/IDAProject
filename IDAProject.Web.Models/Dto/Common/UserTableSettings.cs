namespace IDAProject.Web.Models.Dto.Common
{
    public class UserTableSettings
    {
        public UserTableSettings() : this(string.Empty)
        {
        }

        public UserTableSettings(string tableName)
        {
            HiddenColumnsChangedByUser = false;
            TableName = tableName;
            HiddenColumns = new List<string>();
            CustomColumnsOrder = new List<string>();
        }

        public bool HiddenColumnsChangedByUser { get; set; }
        public string TableName { get; set; }

        public List<string> HiddenColumns { get; set; }

        public List<string> CustomColumnsOrder { get; set; }
    }
}