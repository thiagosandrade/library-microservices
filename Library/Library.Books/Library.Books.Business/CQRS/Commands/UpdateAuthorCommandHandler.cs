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
    public class UpdateAuthorCommandHandler : BaseHandler<Author>, IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IEventBus _eventBus;

        public UpdateAuthorCommandHandler(IMapper mapper, IGenericRepository<Author> authorRepository, IEventBus eventBus) : base(mapper, authorRepository)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var checkAuthor = await Repository.GetById(request.Id, false);

            Mapper.Map(request, checkAuthor);

            if (checkAuthor is null)
                throw new Exception("Author not found");

            await Repository.Update(request.Id, checkAuthor);

            var @event = new AuthorUpdatedEvent($"Author {request.Name} updated", null, new string[] { request.User });

            await _eventBus.PublishMessage<AuthorUpdatedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
