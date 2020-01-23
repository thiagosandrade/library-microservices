using MediatR;
using System;

namespace Library.Authors.Business.CQRS.Contracts.Queries
{
    public class GetAuthorQuery : IRequest<GetAuthorQueryResult>
    {
        public Guid AuthorId { get; set; }
    }
}
