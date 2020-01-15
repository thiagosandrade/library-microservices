﻿using System.Threading.Tasks;
using Library.Authors.Business.Events;
using Library.Authors.Rabbit.RabbitMq;
using Microsoft.Extensions.Logging;

namespace Library.Authors.Business.Handlers
{
    public class BookCreatedEventHandler : IMessageEventHandler<BookCreatedEvent>
    {
        private readonly ILogger<BookCreatedEventHandler> _logger;

        public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(BookCreatedEvent @event)
        {
            _logger.LogInformation("BookCreatedEventHandler {0}", @event);
            return Task.Run(() =>
            {
                _logger.LogInformation($"EventId: {@event.Id} - EventName: {@event.Name}");
            });
        }
    }
}
