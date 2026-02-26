using IDAProject.Web.Models.General;

namespace IDAProject.Web.Models.RequestModels.MasterData
{
    public class MasterEntityFieldRequestModel : MasterEntityFieldBase
    {
        public MasterEntityFieldRequestModel(): base()
        {
            Value = string.Empty;
        }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name} => {Value}";
        }
    }
}
