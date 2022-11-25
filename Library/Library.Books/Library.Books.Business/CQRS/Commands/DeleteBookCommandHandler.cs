using System;
using System.Dynamic;
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
    public class DeleteBookCommandHandler : BaseHandler<Book>, IRequestHandler<DeleteBookCommand>
    {
        private readonly IEventBus _eventBus;

        public DeleteBookCommandHandler(IMapper mapper, IGenericRepository<Book> bookRepository,
            IEventBus eventBus) : base(mapper, bookRepository)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await Repository.GetById(request.BookId, true);

            if (book is null)
            {
                throw new Exception("Book not found");
            }

            await Repository.Delete(book.Id);

            dynamic item = new ExpandoObject();
            item.BookId = book.Id;

            var @event = new BookDeletedEvent($"Book {book.Title} deleted", item);

            await _eventBus.PublishMessage<BookDeletedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
