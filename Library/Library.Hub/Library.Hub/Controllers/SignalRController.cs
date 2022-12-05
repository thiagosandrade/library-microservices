using Library.Hub.Core.SignalR.Library.Hub.Core.SignalR;
using Library.Hub.SignalR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.Hub.Controllers
{
    [Route("api/[controller]")]
    public class SignalRController : Controller
    {
        private readonly INotificationHub _notificationHub;

        public SignalRController(INotificationHub notificationHub)
        {
            _notificationHub = notificationHub;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SignalRMessage message)
        {
            try
            {
                await _notificationHub.SendMessage(message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
