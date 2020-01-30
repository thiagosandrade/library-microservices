using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Authors.Business.CQRS.Contracts.Queries;
using Library.Authors.Database.Interfaces;
using Library.Authors.Domain.Models;
using MediatR;

namespace Library.Authors.Business.CQRS.Queries
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
