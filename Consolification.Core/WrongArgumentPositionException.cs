using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class WrongArgumentPositionException : Exception
    {
        public WrongArgumentPositionException(string parentArgument, string childArgument)
            : base(string.Format("The argument '{0}' must be placed before the argument '{1}'.", parentArgument, childArgument))
        {
        }

        protected WrongArgumentPositionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
