using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.EmployeeViewTrackings
{
    public class SaveEmployeeViewTrackingRequestModel
    {
        public Int32 Id { get; set; }
public Int32? ViewerEmployeeId { get; set; }
public Int32? ViewedEmployeeId { get; set; }
public DateTime? ViewedFrom { get; set; }
public DateTime? ViewedUntil { get; set; }
        public bool IsBookmarked { get; set; }

    }
}
