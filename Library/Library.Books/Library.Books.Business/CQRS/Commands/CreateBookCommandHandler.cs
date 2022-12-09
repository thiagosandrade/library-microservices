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
    public class CreateBookCommandHandler : BaseHandler<Book>, IRequestHandler<CreateBookCommand>
    {
        private readonly IDaprHandler _daprHandler;

        public CreateBookCommandHandler(IMapper mapper, IGenericRepository<Book> bookRepository,
             IDaprHandler daprHandler) : base(mapper, bookRepository)
        {
            _daprHandler = daprHandler;
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            Book book = Mapper.Map<Book>(request);

            await Repository.Create(book);

            var @event = new MessageEvent($"Book {request.Title} created", null, new string[] { request.User });

            await _daprHandler.PublishMessage<MessageEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
