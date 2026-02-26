using IDAProject.Web.Models.General;

namespace IDAProject.Web.Models.Dto.MasterData
{
    public class MasterEntityField : MasterEntityFieldBase
    {
        public MasterEntityField() : base()
        {       
            DisplayName = string.Empty;
            Type = string.Empty;
            Options = new List<GenericSelectOption>();
        }
   
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public IEnumerable<GenericSelectOption> Options { get; set; }
    }
}