using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class MissingParentArgumentException : Exception
    {
        public MissingParentArgumentException(string parentArgument) 
            : base(string.Format("The parent argument '{0}' is missing.", parentArgument))
        {
        }

        public MissingParentArgumentException(string parentArgument, string childArgument)
            : base(string.Format("The parent argument '{0}' is missing for the argument '{1}'.", parentArgument, childArgument))
        {
        }

        protected MissingParentArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
