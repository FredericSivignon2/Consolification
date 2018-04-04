using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class VeryComplexChild2DataMock
    {
        [CIMandatoryArgument]
        [CINamedArgument("/INTARG2")]
        public int IntArg2 { get; private set; }

        [CINamedArgument("/STRARG2")]
        public string StrArg2 { get; private set; }
    }
}
