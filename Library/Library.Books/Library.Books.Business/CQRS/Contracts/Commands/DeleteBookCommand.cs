using MediatR;

namespace Library.Books.Business.CQRS.Contracts.Commands
{
    public class DeleteBookCommand : IRequest
    {
        public int BookId { get; set; }
    }
}
