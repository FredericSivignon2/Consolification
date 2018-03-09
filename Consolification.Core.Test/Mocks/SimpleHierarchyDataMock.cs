using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class SimpleHierarchyDataMock
    {
        [CINamedArgument("/TOP")]
        [CIParentArgument(1)]
        public string TopArg { get; private set; }

        [CINamedArgument("/MID")]
        [CIParentArgument(2)]
        [CIChildArgument(1)]
        public string MidArg { get; private set; }

        [CINamedArgument("/CHILD1")]
        [CIChildArgument(1)]
        public string ChildArg1 { get; private set; }

        [CINamedArgument("/CHILD2")]
        [CIChildArgument(2)]
        [CIMandatoryArgument]
        public string ChildArg2 { get; private set; }

        [CINamedArgument("/CHILD3")]
        [CIChildArgument(2)]
        public string ChildArg3 { get; private set; }
    }
}
