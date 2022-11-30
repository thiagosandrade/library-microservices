using System.Threading;
using System.Threading.Tasks;
using Library.Hub.Rabbit.RabbitMq;
using Library.Shop.Business.CQRS.Contracts.Commands;
using Library.Shop.Business.Events;
using Library.Shop.Database.Interfaces;
using Library.Shop.Domain.Models;
using MediatR;

namespace Library.Shop.Business.CQRS.Commands
{
    public class RemoveProductCommandHandler : BaseHandler, IRequestHandler<RemoveProductCommand>
    {
        private readonly IEventBus _eventBus;

        private readonly IGenericRepository<Cart> _cartRepository;

        public RemoveProductCommandHandler(IGenericRepository<Cart> cartRepository, IEventBus eventBus)
        {
            _cartRepository = cartRepository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetById(request.Id);

            cart.RemoveItem(request.ProductId, request.Quantity);

            await _cartRepository.Update(request.Id, cart);

            var @event = new CartProductRemovedEvent(message: $"Product removed from cart", null, new string[] { request.User });

            await _eventBus.PublishMessage<CartProductRemovedEvent>(@event);

            return Unit.Value;
        }
    }
}
