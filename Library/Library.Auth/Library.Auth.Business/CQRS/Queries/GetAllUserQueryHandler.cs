using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Auth.Business.CQRS.Contracts.Queries;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using MediatR;

namespace Library.Auth.Business.CQRS.Queries
{
    public class GetAllUserQueryHandler : BaseHandler, IRequestHandler<GetAllUserQuery, GetAllUserQueryResult>
    {

        public GetAllUserQueryHandler(IMapper mapper, IGenericRepository<User> userRepository) : base(mapper, userRepository)
        {
        }

        public async Task<GetAllUserQueryResult> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var result = await UserRepository.GetAll();

            return Mapper.Map<GetAllUserQueryResult>(result);
        }
    }
}
