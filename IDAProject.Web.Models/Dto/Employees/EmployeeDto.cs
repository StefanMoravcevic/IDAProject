using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Employees
{
    public class EmployeeDto : SaveEmployeeRequestModel
    {
        public EmployeeDto()
        {
        }
        public string? Dispatcher { get; set; }
        public int? DispatcherId { get; set; }
        #region Basic data


        public string? JobType { get; set; }
        public string? Company { get; set; }
        public string? OrgUnit { get; set; }
        public string? Partner { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? StateShort { get; set; }
        public string? Gender { get; set; }
        public string? Sector { get; set; }
        public int? UserId { get; set; }
        
        public string BirthDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(BirthDate); }
        }

        #endregion

    }
}
