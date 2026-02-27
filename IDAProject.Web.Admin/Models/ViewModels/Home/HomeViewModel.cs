
namespace IDAProject.Web.Admin.Models.ViewModels.Home
{
    public class HomeViewModel : NavigationBaseViewModel
    {
        public HomeViewModel()
        {
            //VehiclesAvailability = new List<VehiclesAvailabilityDto>();
            //DriversAvailability = new List<DriversAvailabilityDto>();
            //DefaultDateFrom = DateTime.Now;
        }

        //public List<DriversAvailabilityDto> DriversAvailability { get; set; }
        //public List<VehiclesAvailabilityDto> VehiclesAvailability { get; set; }


        public string? EmployeePhoto { get; internal set; }
    }
}
