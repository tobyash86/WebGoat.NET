using System;
using System.Runtime.Serialization;

namespace WebGoatCore.Exceptions
{
    public class WebGoatCreditCardNotFoundException : AbstractWebGoatException
    {
        public WebGoatCreditCardNotFoundException()
        {
        }

        public WebGoatCreditCardNotFoundException(string? message) : base(message)
        {
        }

        public WebGoatCreditCardNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public WebGoatCreditCardNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}