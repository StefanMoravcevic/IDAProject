using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Models.Dto.Common
{
    public class CityDto : ISelectOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;


        // Note: Implicit implementation because we want to avoid Json serialization of these properties in API response

        int? ISelectOption.Value
        {
            get { return Id; }
        }

        string ISelectOption.Description
        {
            get { return Name; }
        }
    }
}