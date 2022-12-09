using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Commands;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using Library.Hub.Core.Interfaces;
using Library.Hub.Infrastructure.Events;
using MediatR;

namespace Library.Books.Business.CQRS.Commands
{
    public class CreateAuthorCommandHandler : BaseHandler<Author>, IRequestHandler<CreateAuthorCommand>
    {
        private readonly IDaprHandler _daprHandler;

        public CreateAuthorCommandHandler(IMapper mapper, IGenericRepository<Author> authorRepository,
            IDaprHandler daprHandler) : base(mapper, authorRepository)
        {
            _daprHandler = daprHandler;
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author author = Mapper.Map<Author>(request);

            await Repository.Create(author);

            var @event = new MessageEvent($"Author {request.Name} created", null, new string[] { request.User });

            await _daprHandler.PublishMessage<MessageEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
