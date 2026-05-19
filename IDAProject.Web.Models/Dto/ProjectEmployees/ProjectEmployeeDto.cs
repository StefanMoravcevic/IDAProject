using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.ProjectEmployees
{
    public class ProjectEmployeeDto : SaveProjectEmployeeRequestModel
    {
        public ProjectEmployeeDto()
        {
        }
        #region Basic data

        public string? Employee { get; set; }
        public string? Project { get; set; }
        

        #endregion
    }
}
