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
    public class UpdateBookCommandHandler : BaseHandler<Book>, IRequestHandler<UpdateBookCommand>
    {
        private readonly IEventBus _eventBus;

        public UpdateBookCommandHandler(IMapper mapper, IGenericRepository<Book> bookRepository, IEventBus eventBus) : base(mapper, bookRepository)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var checkBook = await Repository.GetById(request.Id, false, x => x.Authors, x => x.Categories);

            Mapper.Map(request, checkBook);

            if (checkBook is null)
                throw new Exception("Book not found");

            await Repository.Update(request.Id, checkBook);

            var @event = new BookUpdatedEvent($"Book {request.Title} updated", null, new string[] { request.User });

            await _eventBus.PublishMessage<BookUpdatedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
