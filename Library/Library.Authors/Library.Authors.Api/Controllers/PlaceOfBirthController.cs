using System.Threading.Tasks;
using Library.Authors.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Authors.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceOfBirthController : Controller
    {
        private ILogger<PlaceOfBirthController> _logger;
        private IMediator _mediator;

        public PlaceOfBirthController(ILogger<PlaceOfBirthController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetAllPlaceOfBirthQueryResult> Get()
        {
            return await _mediator.Send(new GetAllPlaceOfBirthQuery());
        }
    }
}