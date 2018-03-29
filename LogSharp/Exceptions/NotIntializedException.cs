using System;
using System.Runtime.Serialization;

namespace LogSharp.Exceptions
{
    [Serializable]
    public class NotIntializedException : Exception
    {
        public NotIntializedException()
        {
        }

        public NotIntializedException(string message) : base(message)
        {
        }

        public NotIntializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotIntializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
     }
}