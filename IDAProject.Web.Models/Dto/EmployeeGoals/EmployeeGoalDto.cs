using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.EmployeeGoals
{
    public class EmployeeGoalDto : SaveEmployeeGoalRequestModel
    {
        public EmployeeGoalDto()
        {
        }
        #region Basic data

        public string? Employee { get; set; }
        public int? Year { get; set; }

        #endregion
    }
}
