using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksPlannings
{
    public class EmployeePlanningStatsDto
    {
        public int? TotalPlannedDays { get; set; }

        public int? PlannedOnTimeDays { get; set; }

        public decimal Percentage { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? EmployeeName { get; set; }
    }
}
