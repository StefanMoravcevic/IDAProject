using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.IdaTasks
{
    public class IdaTaskViewModel : NavigationBaseViewModel
    {
        public IdaTaskViewModel()
        {
            IdaTask = new IdaTaskDto();
        }
        public IdaTaskDto IdaTask { get; set; }

    }
}
