using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.Projects
{
    public class SaveProjectRequestModel
    {
        public Int32 Id { get; set; }
public String Description { get; set; }
public Boolean IsCompleted { get; set; }

    }
}
