using Library.Shop.Api.Requests;
using Library.Shop.Business.CQRS.Contracts.Commands;
using Library.Shop.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet("")]
        public async Task<IActionResult> GetCart(int userId)
        {
            _logger.LogInformation($"Get Cart");

            var result = await _mediator.Send(new GetCartQuery(userId));

            return Ok(new OkObjectResult(result));

        }

        [HttpPost("/Product")]
        public async Task<IActionResult> AddToCart([FromBody] ProductRequest product)
        {
            _logger.LogInformation($"Add to cart: {product}");

            await _mediator.Send(new AddProductCommand(product.CartId, product.ProductId, product.Quantity));

            return Ok(new OkObjectResult("Command Completed"));

        }

        [HttpDelete("/Product")]
        public async Task<IActionResult> RemoveFromCart([FromBody] ProductRequest product)
        {
            _logger.LogInformation($"Remove from cart: {product}");

            await _mediator.Send(new RemoveProductCommand(product.CartId, product.ProductId, product.Quantity));

            return Ok(new OkObjectResult("Command Completed"));
        }
    }
}
