﻿using Library.Hub.Core.Interfaces;
using Library.Hub.Logging.Events;
using Microsoft.Extensions.Logging;
using System;

namespace Library.Hub.Logging.Setup
{
    public class CustomLogger<T> : ILogger<T> where T : class
    {
        private readonly IDaprHandler _daprHandler = default!;

        public CustomLogger(IServiceProvider serviceProvider)
        {
            _daprHandler = (IDaprHandler)serviceProvider.GetService(typeof(IDaprHandler));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default!;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception = null, Func<TState, Exception, string> formatter = null)
        {
            if (_daprHandler != null)
            {
                _daprHandler.PublishMessage(new LogMessageEvent()
                {
                    LogLevel = logLevel,
                    Exception = exception,
                    BusinessKey = typeof(T).Name,
                    Message = state.ToString()
                });
            }
            else
            {
                Console.WriteLine(exception);
            }

        }
    }
}
