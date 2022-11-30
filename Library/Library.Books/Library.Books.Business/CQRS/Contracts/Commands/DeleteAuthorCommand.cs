using MediatR;

namespace Library.Books.Business.CQRS.Contracts.Commands
{
    public class DeleteAuthorCommand : IRequest
    {
        public int AuthorId { get; set; }
        public string User { get; set; }
    }
}
