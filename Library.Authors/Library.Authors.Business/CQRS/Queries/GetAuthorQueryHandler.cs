using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Authors.Business.CQRS.Contracts.Queries;
using Library.Authors.Database.Interfaces;
using Library.Authors.Domain.Models;
using MediatR;

namespace Library.Authors.Business.CQRS.Queries
{
    public class GetAuthorQueryHandler : BaseHandler, IRequestHandler<GetAuthorQuery, GetAuthorQueryResult>
    {
        private readonly IGenericRepository<Author> _authorRepository;

        public GetAuthorQueryHandler(IMapper mapper, IGenericRepository<Author> authorRepository) : base(mapper)
        {
            _authorRepository = authorRepository;
        }

        public async Task<GetAuthorQueryResult> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorRepository.GetById(request.AuthorId);

            return Mapper.Map<GetAuthorQueryResult>(result);
        }
    }
}
