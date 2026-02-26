using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Models.Dto.OrderLines;

namespace IDAProject.Web.Api.Models.Interfaces.Hubs
{
    public interface IOrderLinesClient
    {
        Task SearchOrderLines();
    }
}
