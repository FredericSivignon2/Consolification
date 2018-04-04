using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{ 
    [Serializable]
    public class DuplicateArgumentDefinitionException : Exception
    {
        public DuplicateArgumentDefinitionException(string argName) :
            base(string.Format("The definition of the argument '{0}' already exists.", argName))
        {

        }

        protected DuplicateArgumentDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
