using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Authors.Business.CQRS.Contracts.Queries;
using Library.Authors.Database.Interfaces;
using Library.Authors.Domain.Models;
using MediatR;

namespace Library.Authors.Business.CQRS.Queries
{
    public class GetAllAuthorQueryHandler : BaseHandler, IRequestHandler<GetAllAuthorQuery, GetAllAuthorQueryResult>
    {
        private readonly IGenericRepository<Author> _authorRepository;

        public GetAllAuthorQueryHandler(IMapper mapper, IGenericRepository<Author> authorRepository) : base(mapper)
        {
            _authorRepository = authorRepository;
        }

        public async Task<GetAllAuthorQueryResult> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorRepository.GetAll();

            return Mapper.Map<GetAllAuthorQueryResult>(result);
        }
    }
}
