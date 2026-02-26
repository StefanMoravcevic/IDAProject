namespace IDAProject.Web.Models.Dto.MasterData
{
    public class MasterEntityRecord
    {
        public MasterEntityRecord()
        {
            Id = 0;
            Values = new List<string>();
        }

        public int Id { get; set; }

        public List<string> Values { get; set; }
    }
}
