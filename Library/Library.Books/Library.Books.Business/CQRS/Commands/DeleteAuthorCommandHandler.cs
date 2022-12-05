using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Commands;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using Library.Hub.Infrastructure.Events;
using Library.Hub.Infrastructure.Handlers;
using MediatR;

namespace Library.Books.Business.CQRS.Commands
{
    public class DeleteAuthorCommandHandler : BaseHandler<Author>, IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IDaprHandler _daprHandler;

        public DeleteAuthorCommandHandler(IMapper mapper, IGenericRepository<Author> authorRepository,
            IDaprHandler daprHandler) : base(mapper, authorRepository)
        {
            _daprHandler = daprHandler;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await Repository.GetById(request.AuthorId, true);

            if (author is null)
            {
                throw new Exception("Author not found");
            }

            await Repository.Delete(author.Id);

            var @event = new MessageEvent($"Author {author.Name} deleted", null, new string[] { request.User });

            await _daprHandler.PublishMessage<MessageEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
