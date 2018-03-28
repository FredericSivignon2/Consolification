using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class ExclusiveArgumentException : Exception
    {
        public ExclusiveArgumentException(ArgumentInfo currentArgumentInfo, ArgumentInfo existingArgumentInfo)
            : base(string.Format("The argument '{0}' cannot be used in conjonction with '{1}'.", 
                currentArgumentInfo.Name, existingArgumentInfo.Name))
        {
        }

        protected ExclusiveArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
