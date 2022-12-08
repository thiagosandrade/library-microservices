using System.Threading;
using System.Threading.Tasks;
using Library.Hub.Core.Interfaces;
using Library.Hub.Infrastructure.Events;
using Library.Shop.Business.CQRS.Contracts.Commands;
using Library.Shop.Database.Interfaces;
using Library.Shop.Domain.Models;
using MediatR;

namespace Library.Shop.Business.CQRS.Commands
{
    public class AddProductCommandHandler : BaseHandler, IRequestHandler<AddProductCommand>
    {
        private readonly IDaprHandler _daprHandler;

        private readonly IGenericRepository<Cart> _cartRepository;

        public AddProductCommandHandler(IGenericRepository<Cart> cartRepository, IDaprHandler daprHandler)
        {
            _cartRepository = cartRepository;
            _daprHandler = daprHandler;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetById(request.Id);

            cart.AddItem(request.ProductId, request.Quantity);

            await _cartRepository.Update(request.Id, cart);

            var @event = new MessageEvent(message: $"Product added to cart", null, new string[] { request.User });

            await _daprHandler.PublishMessage<MessageEvent>(@event);

            return Unit.Value;
        }
    }
}
