using System.Threading.Tasks;
using Library.Hub.Events;
using Library.Hub.Rabbit.Events.Interfaces;
using Library.Hub.Rabbit.RabbitMq;
using Library.Shop.Business.Events;
using Library.Shop.Database.Interfaces;
using Microsoft.Extensions.Logging;

namespace Library.Shop.Business.Handlers
{
    public class BookDeletedEventHandler : IMessageEventHandler<BookDeletedEvent>
    {
        private readonly ILogger<BookDeletedEventHandler> _logger;
        private readonly ICartRepository _cartRepository;
        private readonly IEventBus _eventBus;


        public BookDeletedEventHandler(ILogger<BookDeletedEventHandler> logger, ICartRepository cartRepository, IEventBus eventBus)
        {
            _logger = logger;
            _cartRepository = cartRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(BookDeletedEvent @event)
        {
            _logger.LogInformation($"{nameof(BookDeletedEventHandler)} {@event}");

            await Task.Run(async () =>
            {
                await _cartRepository.CleanItemsFromCartWhenBookDeleted((int)@event.Item.BookId);

                _logger.LogInformation($"EventMessage: {@event.Message}");

                var @eventCleaned = new CartProductCleanedEvent(message: $"Book removed, cleaning the cart", null, null);

                await _eventBus.PublishMessage<CartProductCleanedEvent>(@eventCleaned);
            });
        }
    }
}
