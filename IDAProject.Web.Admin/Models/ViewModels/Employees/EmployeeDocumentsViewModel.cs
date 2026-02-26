using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Employees
{
    public class EmployeeDocumentsViewModel : NavigationBaseViewModel //NavigationWithAjaxTableViewModel
    {
        public EmployeeDocumentsViewModel()
        {
            DocumentTypes = new List<ISelectOption>();
            DocumentsDownloadUrl = String.Empty;
        }

        public IEnumerable<ISelectOption> DocumentTypes { get; set; }
        public string DocumentsDownloadUrl { get; set; }
        public string? EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        //public UserAccount? User { get; set; }
    }
}
