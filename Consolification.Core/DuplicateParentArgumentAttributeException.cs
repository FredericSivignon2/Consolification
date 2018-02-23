using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    [Serializable]
    public class DuplicateParentArgumentAttributeException : Exception
    {
        public DuplicateParentArgumentAttributeException(int parentId, string propertyName)
            : base(string.Format("The Parent argument identifier '{0}' defined in the CIParentArgumentAttribute associated with the property '{1}' already exists.", parentId, propertyName))
        {
        }

        protected DuplicateParentArgumentAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
