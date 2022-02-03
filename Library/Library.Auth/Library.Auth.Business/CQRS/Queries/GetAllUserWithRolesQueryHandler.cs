using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Auth.Business.CQRS.Contracts.Queries;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using MediatR;

namespace Library.Auth.Business.CQRS.Queries
{
    public class GetAllUserWithRolesQueryHandler : BaseHandler, IRequestHandler<GetAllUserWithRolesQuery, GetAllUserWithRolesQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepository;

        public GetAllUserWithRolesQueryHandler(IMapper mapper, IGenericRepository<User> userRepository) : base()
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GetAllUserWithRolesQueryResult> Handle(GetAllUserWithRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAll(x => x.UserRoles);

            return _mapper.Map<GetAllUserWithRolesQueryResult>(result);
        }
    }
}
