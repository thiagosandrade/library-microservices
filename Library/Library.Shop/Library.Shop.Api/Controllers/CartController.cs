using Library.Shop.Api.Requests;
using Library.Shop.Business.CQRS.Contracts.Commands;
using Library.Shop.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Shop.Api.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Reader, SuperUser")]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IMediator _mediator;

        public CartController(ILogger<CartController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            _logger.LogInformation($"Get Cart");

            var result = await _mediator.Send(new GetCartQuery(userId));

            return Ok(new OkObjectResult(result));

        }

        [HttpPost("Product/AddItem")]
        public async Task<IActionResult> AddToCart([FromBody] ProductRequest product)
        {
            _logger.LogInformation($"Add to cart: {product}");

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(await HttpContext.GetTokenAsync("access_token"));

            var user = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;

            await _mediator.Send(new AddProductCommand(product.CartId, product.ProductId, product.Quantity, user));

            return Ok(new OkObjectResult("Command Completed"));

        }

        [HttpPost("Product/RemoveItem")]
        public async Task<IActionResult> RemoveFromCart([FromBody] ProductRequest product)
        {
            _logger.LogInformation($"Remove from cart: {product}");

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(await HttpContext.GetTokenAsync("access_token"));

            var user = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;

            await _mediator.Send(new RemoveProductCommand(product.CartId, product.ProductId, product.Quantity, user));

            return Ok(new OkObjectResult("Command Completed"));
        }
    }
}
