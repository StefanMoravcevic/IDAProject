using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.EmployeeAbsences
{
    public class SearchEmployeeAbsencesParams
    {
        public int? Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? JobTypeId { get; set; }
        public int? AbsenceTypeId { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsFromHomePage { get; set; }
        //<<SearchParams>>
    }
}

