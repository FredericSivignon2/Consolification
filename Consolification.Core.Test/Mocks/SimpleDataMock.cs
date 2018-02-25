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

        [CIArgument("/SB")]
        public byte MySByte { get; set; }

        [CIArgument("/C1")]
        [CIArgumentBoundary("a", "z")]
        public char MyChar{ get; set; }

        [CIShortcutArgument("/INTEGER16", "/I16")]
        public short MyShort1 { get; set; }
        
        [CIArgument("/I32")]
        public int MyInteger1 { get; set; }

        [CIArgument("/I64")]
        public long MyLong1 { get; set; }

        [CIArgument("/UI16")]
        public ushort MyUShort { get; set; }
        [CIArgument("/UI32")]
        public uint MyUInt { get; set; }
        [CIArgument("/UI64")]
        public ulong MyULong { get; set; }

        [CIArgument("/DEC")]
        public decimal MyDecimal { get; set; }

        [CIArgument("/SINGLE")]
        public float MySingle { get; set; }

        [CIArgument("/D")]
        public double MyDouble1 { get; set; }
        
        [CIArgument("/S")]
        public string MyString1 { get; set; }

        [CIArgument("/STARTDATE")]
        public DateTime StartDate { get; set; }
    }
}
