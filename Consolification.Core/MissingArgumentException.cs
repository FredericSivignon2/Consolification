using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class MissingArgumentException : Exception
    {
        public MissingArgumentException(string argument) 
            : base(string.Format("The mandatory argument {0} is missing.", argument))
        {
        }

        protected MissingArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
