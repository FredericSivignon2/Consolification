using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class ExChildDataMock
    {
        [CINamedArgument("/DC1")]
        [CIExclusiveArgument(1)]
        public int DataC1 { get; private set; }

        [CINamedArgument("/DC2")]
        [CIExclusiveArgument(1)]
        public int DataC2 { get; private set; }

        [CINamedArgument("/DC3")]
        [CIExclusiveArgument]
        public int DataC3 { get; private set; }

        [CINamedArgument("/DC4")]
        public int DataC4 { get; private set; }
    }
}
