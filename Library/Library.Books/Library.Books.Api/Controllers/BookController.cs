﻿using System;
using System.Threading.Tasks;
using Library.Books.Business.CQRS.Contracts.Commands;
using Library.Books.Business.CQRS.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;

        public BookController(ILogger<BookController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<GetBookQueryResult> Get(Guid id)
        {
            var query = new GetBookQuery()
            {
                BookId = id
            };

            return await _mediator.Send(query);
        }

        [HttpGet]
        public async Task<GetAllBookQueryResult> Get()
        {
            return await _mediator.Send(new GetAllBookQuery());
        }

        [HttpPost]
        public async Task Post([FromBody] CreateBookCommand command)
        {
            _logger.LogInformation("Command received: {0}", command);

            await _mediator.Send(command);
        }
    }
}
