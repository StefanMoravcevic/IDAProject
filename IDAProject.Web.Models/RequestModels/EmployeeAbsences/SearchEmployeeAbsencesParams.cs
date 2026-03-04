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
        //<<SearchParams>>
    }
}
