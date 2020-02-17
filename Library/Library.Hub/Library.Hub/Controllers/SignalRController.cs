using Library.Hub.Core.SignalR.Library.Hub.Core.SignalR;
using Library.Hub.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Library.Hub.Controllers
{
    [Route("api/SignalR")]
    public class SignalRController : Controller
    {
        private readonly IHubContext<SignalRHub> _hubContext;

        public SignalRController(IHubContext<SignalRHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SignalRMessage message)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("BroadcastMessage", message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
