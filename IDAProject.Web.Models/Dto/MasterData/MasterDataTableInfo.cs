namespace IDAProject.Web.Models.Dto.MasterData
{
    public class MasterDataTableInfo
    {
        public MasterDataTableInfo() : this(string.Empty, string.Empty)
        {
        }

        public MasterDataTableInfo(string tableName, string description)
        {
            TableName = tableName;
            Description = description;
        }

        public string TableName { get; set; }
        public string Description { get; set; }
    }
}