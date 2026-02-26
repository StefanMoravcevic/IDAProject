namespace IDAProject.Web.Models.RequestModels.MasterData
{
    public class UpdateTableSettingsColumnVisibilityRequestModel
    {
        public UpdateTableSettingsColumnVisibilityRequestModel()
        {
            TableName = string.Empty;
            ColumnName = string.Empty;
            IsVisible = true;
        }

        public int IdUser { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public bool IsVisible { get; set; }
    }
}
