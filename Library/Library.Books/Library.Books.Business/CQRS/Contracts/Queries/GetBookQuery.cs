using MediatR;
using System;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetBookQuery : IRequest<GetBookQueryResult>
    {
        public int BookId { get; set; }
    }
}
