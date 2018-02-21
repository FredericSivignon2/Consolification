using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class SimpleDataMock : ArgumentsContainer
    {
        [CIArgument("/A")]
        public bool MyBoolean1 { get; set; }

        [CIArgument("/B")]
        public byte MyByte1 { get; set; }

        [CIArgument("/B2")]
        [CIArgumentBoundary("10", "40")]
        public byte MyByte2 { get; set; }

        [CIShortcutArgument("/INTEGER16", "/I16")]
        public int MyShort1 { get; set; }
        
        [CIArgument("/I32")]
        public int MyInteger1 { get; set; }

        [CIArgument("/I64")]
        public long MyLong1 { get; set; }

        [CIArgument("/D")]
        public double MyDouble1 { get; set; }
        
        [CIArgument("/S")]
        public string MyString1 { get; set; }
    }
}
