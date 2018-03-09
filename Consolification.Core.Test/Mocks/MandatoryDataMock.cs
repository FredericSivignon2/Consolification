using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class MandatoryDataMock
    {
        [CINamedArgument("/D1")]
        public int Data1 { get; set; }

        [CINamedArgument("/D2")]
        [CIMandatoryArgument]
        public int Data2 { get; set; }

        [CINamedArgument("/D3")]
        [CIMandatoryArgument(true, "")]
        public int Data3 { get; set; }

        [CINamedArgument("/D4")]
        [CIMandatoryArgument(true, "")]
        [CIPassword('-')]
        public string Data4 { get; set; }
    }
}
