using Microsoft.AspNetCore.Mvc;

namespace Library.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        [HttpGet]
        public OkObjectResult Get()
        {
            return Ok("Gateway API");
        }
    }
}