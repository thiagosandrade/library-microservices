using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Auth.Business.CQRS.Contracts.Queries;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using MediatR;

namespace Library.Auth.Business.CQRS.Queries
{
    public class GetUserQueryHandler : BaseHandler, IRequestHandler<GetUserQuery, GetUserQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepository;

        public GetUserQueryHandler(IMapper mapper, IGenericRepository<User> userRepository) : base()
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAll(predicate: x => x.Email.Equals(request.Email), includes: x => x.UserRoles);

            return _mapper.Map<GetUserQueryResult>(result.FirstOrDefault());
        }
    }
}
