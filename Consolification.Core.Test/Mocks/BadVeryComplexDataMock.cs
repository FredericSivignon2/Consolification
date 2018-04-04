using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class BadVeryComplexDataMock
    {
        [CINamedArgument("/COMPLEX")]
        public VeryComplexChildDataMock ComplexChild { get; private set; }
        [CINamedArgument("/COMPLEX2")]
        public VeryComplexChild2DataMock ComplexChild2 { get; private set; }
    }
}
