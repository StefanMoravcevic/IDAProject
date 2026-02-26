namespace IDAProject.Web.Models.RequestModels.MasterData
{
    public class UpdateTableSettingsColumnsOrderRequestModel
    {
        public UpdateTableSettingsColumnsOrderRequestModel()
        {
            TableName = string.Empty;
            Columns = new List<string>();
        }

        public int IdUser { get; set; }
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
    }
}
