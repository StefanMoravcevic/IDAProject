using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.EmployeeJobTypeControls
{
    public class EmployeeJobTypeControlDto : SaveEmployeeJobTypeControlRequestModel
    {
        public EmployeeJobTypeControlDto()
        {
        }
        #region Basic data

        public string? Employee { get; set; }
        public string? JobType { get; set; }
        public string? EmployeeToSee { get; set; }

        #endregion
    }
}
