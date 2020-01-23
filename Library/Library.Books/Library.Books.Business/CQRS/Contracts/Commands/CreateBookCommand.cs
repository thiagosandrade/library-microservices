using System;
using Library.Books.Business.CQRS.Contracts.Queries;
using MediatR;

namespace Library.Books.Business.CQRS.Contracts.Commands
{
    public class CreateBookCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public Guid CategoryId { get; set; }
        public virtual GetCategoryResult Category { get; set; }
    }
}
