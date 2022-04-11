using System;
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
    public class DeleteAuthorCommandHandler : BaseHandler<Author>, IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IEventBus _eventBus;

        public DeleteAuthorCommandHandler(IMapper mapper, IGenericRepository<Author> authorRepository,
            IEventBus eventBus) : base(mapper, authorRepository)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await Repository.GetById(request.AuthorId, true);

            if (author is null)
            {
                throw new Exception("Author not found");
            }

            await Repository.Delete(author.Id);

            var @event = new AuthorDeletedEvent($"Author {author.Name} deleted");

            await _eventBus.PublishMessage<AuthorDeletedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
