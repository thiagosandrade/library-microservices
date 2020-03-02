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
    public class CreateBookCommandHandler : BaseHandler, IRequestHandler<CreateBookCommand>
    {
        private readonly IEventBus _eventBus;

        public CreateBookCommandHandler(IMapper mapper, IGenericRepository<Book> bookRepository,
            IEventBus eventBus) : base(mapper, bookRepository)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {

            Book book = new Book(null, request.Title, request.Isbn, request.PageCount, request.PublishedDate, 
                request.ThumbnailUrl, request.ShortDescription, request.LongDescription,
                request.Status, request.Authors, request.Categories);

            await BookRepository.Create(book);

            var @event = new BookCreatedEvent(book, DateTime.Now);

            await _eventBus.PublishMessage<BookCreatedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
