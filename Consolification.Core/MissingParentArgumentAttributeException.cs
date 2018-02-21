using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class MissingParentArgumentAttributeException : Exception
    {
        public MissingParentArgumentAttributeException(string message) : base(message)
        {
        }

        protected MissingParentArgumentAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
