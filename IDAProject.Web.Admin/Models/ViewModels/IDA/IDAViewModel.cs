using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.IDA
{
    public class IDAViewModel : NavigationBaseViewModel
    {
        public IDAViewModel()
        {
            Projects = new List<GenericSelectOption>();
            ProjectTasks = new List<GenericSelectOption>();
            Tasks = new List<GenericSelectOption>();
            ActivityTypes = new List<GenericSelectOption>();
            PlanStatuses = new List<GenericSelectOption>();
            RegularActivities = new List<GenericSelectOption>();
            TaskPlannings = new List<GenericSelectOption>();
        }

        public IEnumerable<ISelectOption> Projects { get; set; }
        public IEnumerable<ISelectOption> ProjectTasks { get; set; }
        public IEnumerable<ISelectOption> Tasks { get; set; }
        public IEnumerable<ISelectOption> ActivityTypes { get; set; }
        public IEnumerable<ISelectOption> PlanStatuses { get; set; }
        public IEnumerable<ISelectOption> RegularActivities { get; set; }
        public IEnumerable<ISelectOption> TaskPlannings { get; set; }
        public string? Today { get; set; }
        public string? ImageSource { get; set; }
    }
}
