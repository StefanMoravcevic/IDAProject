namespace IDAProject.Web.Models.Dto.MasterData
{
    public class MasterEntity
    {
        public MasterEntity() : this(string.Empty)
        {
        }

        public MasterEntity(string tableName)
        {
            TableName = tableName;
            Fields = new List<MasterEntityField>();
            Records = new List<MasterEntityRecord>();
        }

        public string TableName { get; set; }
        public List<MasterEntityField> Fields { get; set; }
        public List<MasterEntityRecord> Records { get; set; }
    }
}
