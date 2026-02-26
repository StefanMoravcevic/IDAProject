using System.Runtime.CompilerServices;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.General;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Api.Controllers
{
    public class TestController
    {

        public TestController()
        {
        }

        [HttpGet("ping")]
        public string Ping()
        {
            return "Pong";
        }
    }
}
