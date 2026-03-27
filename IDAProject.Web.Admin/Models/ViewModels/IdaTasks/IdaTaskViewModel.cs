using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.IdaTasks
{
    public class IdaTaskViewModel : NavigationBaseViewModel
    {
        public IdaTaskViewModel()
        {
            IdaTask = new List<IdaTaskDto>();
            Projects = new List<GenericSelectOption>();
        }

        public List<IdaTaskDto> IdaTask { get; set; }
        public IEnumerable<ISelectOption> Projects { get; set; }

        public int? UserId { get; set; }

    }
}
