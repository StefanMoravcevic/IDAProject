using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Auth.RequestModels
{
    public class ChangeQuantityRequestModel
    {
        public int? LineId { get; set; }
        public int Quantity { get; set; }
    }
}
