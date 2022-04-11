using System.Threading.Tasks;
using Library.Books.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Books.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Reader, SuperUser")]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;

        public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllCategoryQueryResult>> Get()
        {
            _logger.LogInformation("GetCategories called");

            var result = await _mediator.Send(new GetAllCategoryQuery());

            return Ok(new OkObjectResult(result.Categories));
        }
    }
}
