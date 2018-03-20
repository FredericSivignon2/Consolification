using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    [CIHelpArgument("/?", "This is a complex example of inner class support.")]
    public class ComplexParentDataMock
    {
        [CINamedArgument("/CHILD2", "Child 2")]
        public Child2DataMock Child2Data { get; private set; }

        [CINamedArgument("/SECONDARG", "This is a second argument.", "value")]
        public string SecondArg { get; private set; }
    }
}
