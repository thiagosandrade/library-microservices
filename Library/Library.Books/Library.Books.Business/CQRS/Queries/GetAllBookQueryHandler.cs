using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Queries;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using MediatR;

namespace Library.Books.Business.CQRS.Queries
{
    public class GetAllBookQueryHandler : BaseHandler, IRequestHandler<GetAllBookQuery, GetAllBookQueryResult>
    {
        private readonly IGenericRepository<Book> _bookRepository;

        public GetAllBookQueryHandler(IMapper mapper, IGenericRepository<Book> bookRepository) : base(mapper)
        {
            _bookRepository = bookRepository;
        }

        public async Task<GetAllBookQueryResult> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.GetAll();

            return Mapper.Map<GetAllBookQueryResult>(result);
        }
    }
}
