using MediatR;

namespace Library.Authors.Business.CQRS.Contracts.Queries
{
    public class GetAuthorQuery : IRequest<GetAuthorQueryResult>
    {
        public int AuthorId { get; set; }
    }
}
