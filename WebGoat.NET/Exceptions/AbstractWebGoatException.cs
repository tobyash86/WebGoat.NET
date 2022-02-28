using System;
using System.Runtime.Serialization;

namespace WebGoatCore.Exceptions
{
    public abstract class AbstractWebGoatException : ApplicationException
    {
        protected AbstractWebGoatException()
        {
        }

        protected AbstractWebGoatException(string? message) : base(message)
        {
        }

        protected AbstractWebGoatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected AbstractWebGoatException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}