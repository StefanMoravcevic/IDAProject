using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.EmployeeGoals
{
    public class SaveEmployeeGoalRequestModel
    {
        public Int32 Id { get; set; }
public Int32? EmployeeId { get; set; }
public String? Goal { get; set; }
        public int? YearId { get; set; }

    }
}
