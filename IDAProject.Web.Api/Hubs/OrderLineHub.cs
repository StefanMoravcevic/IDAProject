using Microsoft.AspNetCore.SignalR;
using IDAProject.Web.Api.Models.Interfaces.Hubs;
using IDAProject.Web.Api.Models.Interfaces.Managers;

namespace IDAProject.Web.Api.Hubs
{
    public class OrderLineHub : Hub<IOrderLinesClient>
    {
        private readonly IOrderLinesManager _orderLinesManager;

        public OrderLineHub(IOrderLinesManager orderLinesManager)
        {
            _orderLinesManager = orderLinesManager;
        }
    }
}
