using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.EmployeeViewTrackings
{
    public class SearchEmployeeViewTrackingsParams
    {
        public int? Id { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsBookmarked { get; set; }
        public bool? HideFromHomePage { get; set; }
        public DateTime? Date { get; set; }
        //<<SearchParams>>
    }
}
