using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.IdaTasks
{
    public class SaveIdaTaskRequestModel
    {
        public Int32 Id { get; set; }
public String? Name { get; set; }
public String? Description { get; set; }
public DateTime? DueDate { get; set; }
public Int32? ProjectId { get; set; }
public Boolean IsCompleted { get; set; }

    }
}
