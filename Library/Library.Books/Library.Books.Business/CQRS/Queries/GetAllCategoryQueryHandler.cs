using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Queries;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using MediatR;

namespace Library.Books.Business.CQRS.Queries
{
    public class GetAllCategoryQueryHandler : BaseHandler<Category>, IRequestHandler<GetAllCategoryQuery, GetAllCategoryQueryResult>
    {

        public GetAllCategoryQueryHandler(IMapper mapper, IGenericRepository<Category> categoryRepository) : base(mapper, categoryRepository)
        {
        }

        public async Task<GetAllCategoryQueryResult> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAll();

            return Mapper.Map<GetAllCategoryQueryResult>(result);
        }
    }
}
