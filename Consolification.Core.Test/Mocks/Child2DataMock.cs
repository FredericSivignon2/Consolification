using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    [CIHelpArgument("/?", "This is the child 2.")]
    public class Child2DataMock
    {
        [CIMandatoryArgument]
        [CISimpleArgument("child2data", "Data for child 2.")]
        public string Child2Data { get; private set; }

        [CINamedArgument("/CHILD1", "Child 1 from child 2.")]
        public Child1DataMock Child1Data { get; private set; }
    }
}
