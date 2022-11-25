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
    public class AddProductCommandHandler : BaseHandler, IRequestHandler<AddProductCommand>
    {
        private readonly IEventBus _eventBus;

        private readonly IGenericRepository<Cart> _cartRepository;

        public AddProductCommandHandler(IGenericRepository<Cart> cartRepository, IEventBus eventBus)
        {
            _cartRepository = cartRepository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetById(request.Id);

            cart.AddItem(request.ProductId, request.Quantity);

            await _cartRepository.Update(request.Id, cart);

            var @event = new CartProductAddedEvent(message: $"Product added to cart");

            await _eventBus.PublishMessage<CartProductAddedEvent>(@event);

            return Unit.Value;
        }
    }
}
