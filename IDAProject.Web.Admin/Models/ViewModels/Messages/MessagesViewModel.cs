using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Messages
{
    public class MessagesViewModel : NavigationWithAjaxTableViewModel
    {
        public MessagesViewModel()
        {
            Days = new List<GenericSelectOption>()
            {
                new GenericSelectOption { Value = 1, Description = "1 day" },
                new GenericSelectOption { Value = 2, Description = "2 days" },
                new GenericSelectOption { Value = 3, Description = "3 days" },
                new GenericSelectOption { Value = 4, Description = "4 days" },
                new GenericSelectOption { Value = 5, Description = "5 days" },
                new GenericSelectOption { Value = 7, Description = "Week" },
                new GenericSelectOption { Value = 30, Description = "Month" },
                new GenericSelectOption { Value = 365, Description = "Year" }
            };
            Drivers = new List<ISelectOption>();
            UserMessagesViewModel = new UserMessagesViewModel();
            EmailsViewModel = new EmailsViewModel();
        }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public IEnumerable<ISelectOption> Days { get; set; }
        public IEnumerable<ISelectOption> Drivers { get; set; }
        public EmailsViewModel EmailsViewModel { get; set; }
        public UserMessagesViewModel UserMessagesViewModel { get; set; }
    }
}