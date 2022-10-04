using System;
using System.Runtime.Serialization;

namespace WebGoatCore.Exceptions
{
    public class WebGoatFatalException : AbstractWebGoatException
    {
        public WebGoatFatalException()
        {
        }

        public WebGoatFatalException(string? message) : base(message)
        {
        }

        public WebGoatFatalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public WebGoatFatalException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}