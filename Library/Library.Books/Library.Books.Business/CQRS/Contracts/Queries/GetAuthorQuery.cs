using MediatR;
using System;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetAuthorQuery : IRequest<GetAuthorQueryResult>
    {
        public int AuthorId { get; set; }
    }
}
