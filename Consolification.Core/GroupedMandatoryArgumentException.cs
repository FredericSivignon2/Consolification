using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class GroupedMandatoryArgumentException : Exception
    {
        public GroupedMandatoryArgumentException(string argumentList)
           : base("One of the following arguments is missing: " + argumentList)
        {
        }

        protected GroupedMandatoryArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
