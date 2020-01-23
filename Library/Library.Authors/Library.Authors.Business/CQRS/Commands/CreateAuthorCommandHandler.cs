using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Authors.Business.CQRS.Contracts.Commands;
using Library.Authors.Business.Events;
using Library.Authors.Database.Interfaces;
using Library.Authors.Domain.Models;
using Library.Hub.Rabbit.RabbitMq;
using MediatR;

namespace Library.Authors.Business.CQRS.Commands
{
    public class CreateAuthorCommandHandler : BaseHandler, IRequestHandler<CreateAuthorCommand>
    {
        private readonly IEventBus _eventBus;

        public CreateAuthorCommandHandler(IMapper mapper, IGenericRepository<Author> authorRepository,
            IEventBus eventBus) : base(mapper, authorRepository)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author author = new Author(request.Name, request.Surname, request.Birth, request.PlaceOfBirthId);

            await AuthorRepository.Create(author);

            var @event = new AuthorCreatedEvent(author, DateTime.Now);

            await _eventBus.PublishMessage<AuthorCreatedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
