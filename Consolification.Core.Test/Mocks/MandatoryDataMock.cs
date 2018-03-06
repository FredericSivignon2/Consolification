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
        [CIArgument("/D1")]
        public int Data1 { get; set; }

        [CIArgument("/D2")]
        [CIMandatoryArgument]
        public int Data2 { get; set; }

        [CIArgument("/D3")]
        [CIMandatoryArgument(true, "")]
        public int Data3 { get; set; }

        [CIArgument("/D4")]
        [CIMandatoryArgument(true, "")]
        [CIPassword('-')]
        public string Data4 { get; set; }
    }
}
