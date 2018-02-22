using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class HierarchyDataMock : ArgumentsContainer
    {
        [CIArgument("/TOP")]
        [CIParentArgument(1)]
        public string TopArg { get; private set; }

        [CIArgument("/MID")]
        [CIParentArgument(2)]
        [CIChildArgument(1)]
        public string MidArg { get; private set; }

        [CIArgument("/CHILD1")]
        [CIChildArgument(1)]
        public string ChildArg1 { get; private set; }

        [CIArgument("/CHILD2")]
        [CIChildArgument(2)]
        [CIMandatoryArgument()]
        public string ChildArg2 { get; private set; }

        [CIArgument("/CHILD3")]
        [CIChildArgument(2)]
        public string ChildArg3 { get; private set; }
    }
}
