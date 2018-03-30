using System;
using System.Runtime.Serialization;

namespace LogSharp.Exceptions
{
    /// <summary>
    /// Exception which is thrown when LogManager is used without being
    /// initialized at all.
    /// </summary>
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