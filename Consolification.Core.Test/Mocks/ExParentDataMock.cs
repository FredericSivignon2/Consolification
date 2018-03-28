using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class ExParentDataMock
    {
        [CINamedArgument("/D1")]
        [CIExclusiveArgument(1)]
        public int Data1 { get; private set; }

        [CINamedArgument("/D2")]
        [CIExclusiveArgument(1)]
        public int Data2 { get; private set; }

        [CINamedArgument("/D3")]
        [CIExclusiveArgument]
        public int Data3 { get; private set; }

        [CINamedArgument("/D4")]
        public int Data4 { get; private set; }

        [CINamedArgument("/CHILD")]
        public ExChildDataMock Child { get; private set; }
    }
}
