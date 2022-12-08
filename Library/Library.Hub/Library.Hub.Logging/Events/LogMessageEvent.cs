using Library.Hub.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Library.Hub.Logging.Events
{
    public class LogMessageEvent : IMessageEvent
    {
        public LogLevel LogLevel { get; set; }
        public string BusinessKey { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public LogMessageEvent()
        {

        }

        public LogMessageEvent(string businessKey, string message, LogLevel logLevel, Exception exception)
        {
            BusinessKey = businessKey;
            Message = message;
            LogLevel = logLevel;
            Exception = exception;
        }
    }
}
