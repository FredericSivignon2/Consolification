using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class InvalidArgumentDefinitionException : Exception
    {
        public InvalidArgumentDefinitionException(string message) : base(message)
        {

        }

        protected InvalidArgumentDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
