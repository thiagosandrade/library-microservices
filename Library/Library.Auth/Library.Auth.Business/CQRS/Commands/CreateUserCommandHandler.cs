using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Auth.Business.CQRS.Contracts.Commands;
using Library.Auth.Business.Events;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using Library.Hub.Rabbit.RabbitMq;
using MediatR;

namespace Library.Auth.Business.CQRS.Commands
{
    public class CreateUserCommandHandler : BaseHandler, IRequestHandler<CreateUserCommand>
    {
        private readonly IEventBus _eventBus;

        public CreateUserCommandHandler(IMapper mapper, IGenericRepository<User> userRepository,
            IEventBus eventBus) : base(mapper, userRepository)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(request.Name, request.Surname, request.Login, request.Email, request.Password);

            await UserRepository.Create(user);

            var @event = new UserLoggedEvent(user);

            await _eventBus.PublishMessage<UserLoggedEvent>(@event);

            return await Task.FromResult(Unit.Value);
        }
    }
}
