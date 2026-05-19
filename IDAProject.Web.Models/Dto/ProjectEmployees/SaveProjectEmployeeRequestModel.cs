using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.ProjectEmployees
{
    public class SaveProjectEmployeeRequestModel
    {
        public Int32 Id { get; set; }
public Int32? ProjectId { get; set; }
public Int32? EmployeeId { get; set; }

    }
}
