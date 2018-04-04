using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class VeryComplexChildDataMock
    {
        [CIMandatoryArgument]
        [CINamedArgument("/INTARG1")]
        public int IntArg1 { get; private set; }

        [CINamedArgument("/STRARG1")]
        public string StrArg1 { get; private set; }

        [CINamedArgument("/CHILDVC2")]
        public VeryComplexChild2DataMock ChildVC22 { get; private set; }
    }
}
