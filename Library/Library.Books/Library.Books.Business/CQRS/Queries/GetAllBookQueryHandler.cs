using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Queries;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using MediatR;

namespace Library.Books.Business.CQRS.Queries
{
    public class GetAllBookQueryHandler : BaseHandler<Book>, IRequestHandler<GetAllBookQuery, GetAllBookQueryResult>
    {
        public GetAllBookQueryHandler(IMapper mapper, IGenericRepository<Book> bookRepository) : base(mapper, bookRepository)
        {
        }

        public async Task<GetAllBookQueryResult> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAll(null, x => x.Authors, x => x.Categories);

            var result2 = Mapper.Map<GetAllBookQueryResult>(result);

            return result2;
        }
    }
}
