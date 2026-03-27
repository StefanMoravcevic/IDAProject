using IDAProject.Web.Api.Managers;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class GoogleController : Controller
    {
        private IGoogleManager _googleManager;

        public GoogleController(IGoogleManager googleManager)
        {
            _googleManager = googleManager;
        }

        [HttpGet("getOAuthUrl/{employeeId}")]
        public IActionResult GetOAuthUrl(int employeeId)
        {
            var redirectUri = "http://localhost:5169/api/google/callback";
            var url = _googleManager.GetOAuthUrl(redirectUri, employeeId);
            var response = new ResponseModel<string>
            {
                Valid = true,
                Payload = url
            };
            return Ok(response);
        }
        [HttpGet("handleOAuthCallbackAsync/{code}/{state}/{url}")]
        public async Task<IActionResult> HandleOAuthCallbackAsync(string code, string state, string url)
        {
            var result = new EmployeeDto();
            result = await _googleManager.HandleOAuthCallbackAsync(code, state, url);
            return Json(result);
        }

        [HttpGet("callback")]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Code nije stigao");

            if (string.IsNullOrEmpty(state))
                return BadRequest("State nije stigao");

            int employeeId = int.Parse(state);

            try
            {
                var employee = await _googleManager.HandleOAuthCallbackAsync(
                    code,
                    state,
                    "http://localhost:5169/api/google/callback"
                );

                return Content(@"
<html>
<head>
    <title>Google povezivanje</title>

    <!-- Auto redirect posle 3 sekunde -->
    <meta http-equiv='refresh' content='3;url=https://localhost:7136/' />

    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
            background: linear-gradient(135deg, #4CAF50, #2E7D32);
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .card {
            background: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 15px 40px rgba(0,0,0,0.2);
            text-align: center;
            max-width: 420px;
            animation: fadeIn 0.5s ease-in-out;
        }

        .icon {
            font-size: 60px;
            margin-bottom: 15px;
        }

        h2 {
            margin: 10px 0;
            color: #333;
        }

        p {
            color: #666;
            margin-bottom: 25px;
        }

        a.button {
            display: inline-block;
            padding: 12px 25px;
            background: #4CAF50;
            color: white;
            text-decoration: none;
            border-radius: 8px;
            font-weight: bold;
            transition: 0.3s;
        }

        a.button:hover {
            background: #388E3C;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(10px); }
            to { opacity: 1; transform: translateY(0); }
        }
    </style>
</head>

<body>
    <div class='card'>
        <div class='icon'>✅</div>
        <h2>Uspešno povezano</h2>
        <p>Vaš Google nalog je uspešno povezan.<br/>Bićete preusmereni za par sekundi...</p>
        <a class='button' href='https://localhost:7136/'>Idi odmah</a>
    </div>
</body>
</html>
", "text/html; charset=utf-8");
            }
            catch (Exception ex)
            {
                return Content($@"
<html>
<head>
    <title>Greška</title>
    <style>
        body {{
            margin: 0;
            font-family: Arial;
            background: #f44336;
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }}

        .card {{
            background: white;
            padding: 30px;
            border-radius: 10px;
            text-align: center;
            max-width: 400px;
        }}

        h2 {{
            color: #d32f2f;
        }}

        p {{
            color: #555;
        }}

        a {{
            display: inline-block;
            margin-top: 15px;
            color: white;
            background: #d32f2f;
            padding: 10px 20px;
            border-radius: 6px;
            text-decoration: none;
        }}
    </style>
</head>

<body>
    <div class='card'>
        <h2>❌ Došlo je do greške</h2>
        <p>{ex.Message}</p>
        <a href='https://localhost:7136/'>Nazad na login</a>
    </div>
</body>
</html>
", "text/html; charset=utf-8");
            }
        }

        [HttpGet("todaysEvents/{employeeId}")]
        public async Task<IActionResult> TodaysEvents(int employeeId)
        {
            await _googleManager.SyncFutureEventsForEmployeeAsync(employeeId);
            return Ok(new { message = "Events synchronized successfully." });
        }
    }
}

