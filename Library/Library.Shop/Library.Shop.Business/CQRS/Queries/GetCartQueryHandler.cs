using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Shop.Business.CQRS;
using Library.Shop.Business.CQRS.Contracts.Queries;
using Library.Shop.Database.Interfaces;
using Library.Shop.Domain.Models;
using MediatR;

namespace Library.Shop.Business.CQRS.Queries
{
    public class GetCartQueryHandler : BaseHandler, IRequestHandler<GetCartQuery, GetCartQueryResult>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Cart> _cartRepository;

        public GetCartQueryHandler(IMapper mapper, IGenericRepository<Cart> cartRepository) : base()
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<GetCartQueryResult> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.GetAll(x => x.UserId.Equals(request.UserId));

            if (result.Count.Equals(0))
            {
                await _cartRepository.Create(new Cart(request.UserId));

                var resultAfterCreate = await _cartRepository.GetAll(x => x.UserId.Equals(request.UserId));

                return _mapper.Map<GetCartQueryResult>(resultAfterCreate.FirstOrDefault());
            }

            return _mapper.Map<GetCartQueryResult>(result.FirstOrDefault());
        }
    }
}
