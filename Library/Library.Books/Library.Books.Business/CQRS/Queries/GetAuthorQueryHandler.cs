using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Queries;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using MediatR;

namespace Library.Books.Business.CQRS.Queries
{
    public class GetAuthorQueryHandler : BaseHandler<Author>, IRequestHandler<GetAuthorQuery, GetAuthorQueryResult>
    {
        public GetAuthorQueryHandler(IMapper mapper, IGenericRepository<Author> authorRepository) : base(mapper, authorRepository)
        {
        }

        public async Task<GetAuthorQueryResult> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var result = await Repository.GetById(request.AuthorId);

            return Mapper.Map<GetAuthorQueryResult>(result);
        }
    }
}
