using IDAProject.Web.Models.Dto.Common;

namespace IDAProject.Web.Models.Dto.Employees
{
    public class EmployeeSearchResponseModel
    {
        public EmployeeSearchResponseModel()
        {
            Email = string.Empty;
            Phone = string.Empty;
        }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}