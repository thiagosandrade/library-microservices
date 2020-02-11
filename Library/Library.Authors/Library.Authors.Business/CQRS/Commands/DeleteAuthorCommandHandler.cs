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
            var author = Repository.GetById(request.AuthorId);

            await Repository.Delete(request.AuthorId);

            var @event = new AuthorDeletedEvent(author);

            await _eventBus.PublishMessage<AuthorDeletedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
