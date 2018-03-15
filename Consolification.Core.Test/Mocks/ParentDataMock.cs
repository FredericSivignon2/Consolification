using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class ParentDataMock
    {
        [CINamedArgument("/PARENTVALUE")]
        public int ParentValue { get; private set; }

        [CINamedArgument("/CHILD1")]
        public Child1DataMock Child1 { get; private set; }
    }
}
