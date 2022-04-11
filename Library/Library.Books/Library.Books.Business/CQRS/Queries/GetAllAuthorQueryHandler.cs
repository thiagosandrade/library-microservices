using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Queries;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using MediatR;

namespace Library.Books.Business.CQRS.Queries
{
    public class GetAllAuthorQueryHandler : BaseHandler<Author>, IRequestHandler<GetAllAuthorQuery, GetAllAuthorQueryResult>
    {

        public GetAllAuthorQueryHandler(IMapper mapper, IGenericRepository<Author> authorRepository) : base(mapper, authorRepository)
        {
        }

        public async Task<GetAllAuthorQueryResult> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAll();

            return Mapper.Map<GetAllAuthorQueryResult>(result);
        }
    }
}
