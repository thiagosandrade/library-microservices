using System.Threading.Tasks;
using Library.Books.Business.CQRS.Contracts.Queries;
using MediatR;

namespace Library.Books.Business.CQRS.Contracts.Commands
{
    public class SetBookCommand : IRequest<Task>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public int CategoryId { get; set; }
        public virtual GetCategoryResult Category { get; set; }
    }
}
