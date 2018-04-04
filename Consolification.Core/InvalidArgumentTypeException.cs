using System;
using System.Runtime.Serialization;

namespace Consolification.Core
{
    [Serializable]
    internal class InvalidArgumentTypeException : Exception
    {
        public InvalidArgumentTypeException(string message) : base(message)
        {
        }

        public InvalidArgumentTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidArgumentTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}