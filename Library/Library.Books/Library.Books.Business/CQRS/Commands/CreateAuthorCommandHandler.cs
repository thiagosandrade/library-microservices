using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Commands;
using Library.Books.Business.Events;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using Library.Hub.Rabbit.RabbitMq;
using MediatR;

namespace Library.Books.Business.CQRS.Commands
{
    public class CreateAuthorCommandHandler : BaseHandler<Author>, IRequestHandler<CreateAuthorCommand>
    {
        private readonly IEventBus _eventBus;

        public CreateAuthorCommandHandler(IMapper mapper, IGenericRepository<Author> authorRepository,
            IEventBus eventBus) : base(mapper, authorRepository)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author author = Mapper.Map<Author>(request);

            await Repository.Create(author);

            var @event = new AuthorCreatedEvent($"Author {request.Name} created");

            await _eventBus.PublishMessage<AuthorCreatedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
