using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksRealizations
{
    public class EmployeeRealizationStatsDto
    {
        public int TotalWorkingDays { get; set; }

        public int DaysWithRealization { get; set; }
        public double PlannedCount { get; set; }
        public double UnplannedCount { get; set; }
        public double ProjectCount { get; set; }
        public double TaskCount { get; set; }
        public double RegularCount { get; set; }

        public double TotalWorkHours { get; set; }

        public double TotalLoggedHours { get; set; }

        public double PlannedHours { get; set; }

        public double UnplannedHours { get; set; }

        public decimal PlannedPercentage { get; set; }

        public decimal UnplannedPercentage { get; set; }
        public decimal ProjectPercentage { get; set; }
        public decimal TaskPercentage { get; set; }
        public decimal RegularPercentage { get; set; }
    }
}
