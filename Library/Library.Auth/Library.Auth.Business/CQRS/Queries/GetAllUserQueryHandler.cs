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
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepository;

        public GetAllUserQueryHandler(IMapper mapper, IGenericRepository<User> userRepository) : base()
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GetAllUserQueryResult> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAll();

            return _mapper.Map<GetAllUserQueryResult>(result);
        }
    }
}
