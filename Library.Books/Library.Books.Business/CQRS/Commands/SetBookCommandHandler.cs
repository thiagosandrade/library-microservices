using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Commands;
using MediatR;

namespace Library.Books.Business.CQRS.Commands
{
    public class SetBookCommandHandler : BaseHandler, IRequestHandler<SetBookCommand, Task>
    {
        public SetBookCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public Task<Task> Handle(SetBookCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
