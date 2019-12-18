using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Authors.Business.CQRS.Contracts.Commands;
using MediatR;

namespace Library.Authors.Business.CQRS.Commands
{
    public class UpdateAuthorCommandHandler : BaseHandler, IRequestHandler<UpdateAuthorCommand, Task>
    {
        public UpdateAuthorCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public Task<Task> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
