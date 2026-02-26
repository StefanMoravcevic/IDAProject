namespace IDAProject.Web.Models.RequestModels.MasterData
{
    public class MasterEntityRequestModel
    {
        public MasterEntityRequestModel()
        {
            TableName = string.Empty;
            Fields = new List<MasterEntityFieldRequestModel>();
        }

        public string TableName { get; set; }

        public List<MasterEntityFieldRequestModel> Fields { get; set; }
    }
}
