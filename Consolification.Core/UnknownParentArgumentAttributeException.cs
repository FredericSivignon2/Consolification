using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class UnknownParentArgumentAttributeException : Exception
    {
        public UnknownParentArgumentAttributeException(int parentId, string propertyName) 
            : base(string.Format("The parent argument identifier '{0}' specified in the property '{1}' does not exist.", parentId, propertyName))
        {
        }

        protected UnknownParentArgumentAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
