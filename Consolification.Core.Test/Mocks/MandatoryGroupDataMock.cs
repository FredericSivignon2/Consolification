using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class MandatoryGroupDataMock
    {
        [CINamedArgument("-ARG1")]
        [CIGroupedMandatoryArgumentAttribute(1)]
        public int Arg1 { get; private set; }

        [CINamedArgument("-ARG2")]
        [CIGroupedMandatoryArgumentAttribute(1)]
        public int Arg2 { get; private set; }

        [CINamedArgument("-ARG3")]
        [CIMandatoryArgumentAttribute]
        public int Arg3 { get; private set; }
    }
}
