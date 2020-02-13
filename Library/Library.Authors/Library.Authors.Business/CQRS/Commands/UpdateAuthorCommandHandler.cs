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
    public class UpdateAuthorCommandHandler : BaseHandler<Author>, IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IGenericRepository<PlaceOfBirth> _placeOfBirthRepository;

        private IEventBus _eventBus { get; }

        public UpdateAuthorCommandHandler(IMapper mapper, IGenericRepository<Author> authorRepository, IGenericRepository<PlaceOfBirth> placeOfBirthRepository, IEventBus eventBus) : base(mapper, authorRepository)
        {
            _placeOfBirthRepository = placeOfBirthRepository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author author = Mapper.Map<Author>(request);

            await Repository.Update(request.Id, author);

            var @event = new AuthorUpdatedEvent(author);

            await _eventBus.PublishMessage<AuthorUpdatedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
