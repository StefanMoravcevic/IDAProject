using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.Shifts
{
    public class SaveShiftRequestModel
    {
        public Int32 Id { get; set; }
public Int32 ShiftNo { get; set; }
public TimeOnly? TimeFrom { get; set; }
public TimeOnly? TimeTo { get; set; }

    }
}
