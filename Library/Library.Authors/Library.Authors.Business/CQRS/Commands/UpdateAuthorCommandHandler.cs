using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Authors.Business.CQRS.Contracts.Commands;
using Library.Authors.Database.Interfaces;
using Library.Authors.Domain.Models;
using MediatR;

namespace Library.Authors.Business.CQRS.Commands
{
    public class UpdateAuthorCommandHandler : BaseHandler, IRequestHandler<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandHandler(IMapper mapper, IGenericRepository<Author> authorRepository) : base(mapper, authorRepository)
        {
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author author = new Author(request.Name, request.Surname, request.Birth, request.PlaceOfBirthId, request.AuthorId);

            await AuthorRepository.Update(request.AuthorId, author);

            return await Task.FromResult(Unit.Value);
        }
    }
}
