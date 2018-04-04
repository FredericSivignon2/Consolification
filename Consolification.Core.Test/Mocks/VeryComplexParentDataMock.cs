using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class VeryComplexParentDataMock
    {
        [CINamedArgument("/COMPLEX")]
        public ComplexParentDataMock ParentData { get; private set; }

        [CINamedArgument("/VERYCOMPLEX")]
        public VeryComplexChildDataMock ChildData { get; private set; }
    }
}
