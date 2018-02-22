using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class MissingMandatoryArgumentException : Exception
    {
        public MissingMandatoryArgumentException(string argument) 
            : base(string.Format("The mandatory argument {0} is missing.", argument))
        {
        }

        protected MissingMandatoryArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
