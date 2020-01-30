using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Authors.Business.CQRS.Contracts.Queries;
using Library.Authors.Database.Interfaces;
using Library.Authors.Domain.Models;
using MediatR;

namespace Library.Authors.Business.CQRS.Queries
{
    public class GetAllPlaceOfBirthQueryHandler : BaseHandler<PlaceOfBirth>, IRequestHandler<GetAllPlaceOfBirthQuery, GetAllPlaceOfBirthQueryResult>
    {

        public GetAllPlaceOfBirthQueryHandler(IMapper mapper, IGenericRepository<PlaceOfBirth> placeOfBirthRepository) : base(mapper, placeOfBirthRepository)
        {
        }

        public async Task<GetAllPlaceOfBirthQueryResult> Handle(GetAllPlaceOfBirthQuery request, CancellationToken cancellationToken)
        {
            var result = await Repository.GetAll();

            return Mapper.Map<GetAllPlaceOfBirthQueryResult>(result);
        }
    }
}
