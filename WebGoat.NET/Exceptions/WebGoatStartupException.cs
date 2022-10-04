using System;
using System.Runtime.Serialization;

namespace WebGoatCore.Exceptions
{
    public class WebGoatStartupException : AbstractWebGoatException
    {
        public WebGoatStartupException()
        {
        }

        public WebGoatStartupException(string? message) : base(message)
        {
        }

        public WebGoatStartupException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public WebGoatStartupException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}