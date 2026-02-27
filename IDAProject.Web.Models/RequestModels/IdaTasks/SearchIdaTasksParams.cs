using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.IdaTasks
{
    public class SearchIdaTasksParams
    {
        public int? Id { get; set; }

        public bool? HasProject { get; set; }
        //<<SearchParams>>
    }
}
