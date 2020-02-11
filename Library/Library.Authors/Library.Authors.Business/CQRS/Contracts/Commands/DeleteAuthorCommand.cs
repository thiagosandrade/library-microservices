using MediatR;
using System;

namespace Library.Authors.Business.CQRS.Contracts.Commands
{
    public class DeleteAuthorCommand : IRequest
    {
        public Guid AuthorId { get; set; }
    }
}
