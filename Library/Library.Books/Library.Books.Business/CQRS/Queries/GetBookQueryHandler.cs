using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Queries;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using MediatR;

namespace Library.Books.Business.CQRS.Queries
{
    public class GetBookQueryHandler : BaseHandler, IRequestHandler<GetBookQuery, GetBookQueryResult>
    {
        public GetBookQueryHandler(IMapper mapper, IGenericRepository<Book> bookRepository) : base(mapper, bookRepository)
        {
        }

        public async Task<GetBookQueryResult> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var result = await BookRepository.GetById(request.BookId);

            return Mapper.Map<GetBookQueryResult>(result);
        }
    }
}
