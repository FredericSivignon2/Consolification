using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class Child1DataMock
    {
        [CINamedArgument("/CHILDVALUE1", "This is the child value 1.", "value")]
        public string ChildValue1 { get; private set; }
    }
}
