using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.EmployeeJobTypeControls
{
    public class SaveEmployeeJobTypeControlRequestModel
    {
        public Int32 Id { get; set; }
public Int32? EmployeeId { get; set; }
public Int32? JobTypeId { get; set; }
        public int? EmployeeToSeeId { get; set; }


    }
}
