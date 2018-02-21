using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class UnknownArgumentException : Exception
    {
        public UnknownArgumentException(string argument)
            : base(string.Format("Unknown argument {0}.", argument))
        {
        }

        protected UnknownArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
