using DeclarationFactory.Web.Models.Dto.Shifts;
using DeclarationFactory.Web.Models.General;
using DeclarationFactory.Web.Models.Interfaces.Html;

namespace DeclarationFactory.Web.Admin.Models.ViewModels.Shifts
{
    public class ShiftViewModel : NavigationBaseViewModel
    {
        public ShiftViewModel()
        {
            Shift = new ShiftDto();
        }
        public ShiftDto Shift { get; set; }

    }
}
