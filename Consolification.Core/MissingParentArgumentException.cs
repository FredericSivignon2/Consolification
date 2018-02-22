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
            : base(string.Format("The parent argument {0} is missing.", parentArgument))
        {
        }

        protected MissingParentArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
