using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class MissingArgumentValueException : Exception
    {
        public MissingArgumentValueException(string argument)
            : base(string.Format("The value of the argument '{0}' is missing.", argument))
        {
        }

        protected MissingArgumentValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
