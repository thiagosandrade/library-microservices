using System.Threading.Tasks;
using Library.Hub.Events;
using Library.Hub.Infrastructure.Events;
using Library.Hub.Infrastructure.Events.Interfaces;
using Library.Hub.Infrastructure.Handlers;
using Library.Shop.Database.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Shop.Business.Handlers
{
    public class BookDeletedEventHandler : IMessageEventHandler<BookDeletedEvent>
    {
        private readonly ILogger<BookDeletedEventHandler> _logger;
        private readonly ICartRepository _cartRepository;
        private readonly IDaprHandler _daprHandler;


        public BookDeletedEventHandler(ILogger<BookDeletedEventHandler> logger, ICartRepository cartRepository, IDaprHandler daprHandler)
        {
            _logger = logger;
            _cartRepository = cartRepository;
            _daprHandler = daprHandler;
        }

        public async Task Handle(BookDeletedEvent @event)
        {
            _logger.LogInformation($"{nameof(BookDeletedEventHandler)} {@event}");

            await _cartRepository.CleanItemsFromCartWhenBookDeleted((int)@event.Item.BookId);

            _logger.LogInformation($"EventMessage: {@event.Message}");

            var @eventCleaned = new MessageEvent(message: $"Book removed, cleaning the cart", null, null);

            await _daprHandler.PublishMessage<MessageEvent>(@eventCleaned);
        }
    }
}
