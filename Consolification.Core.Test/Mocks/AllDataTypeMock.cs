using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class AllDataTypeMock
    {
        [CIExclusiveArgument]
        [CINamedArgument("/A")]
        public bool MyBoolean1 { get; set; }

        [CINamedArgument("/B")]
        [CIExclusiveArgument(1)]
        public byte MyByte1 { get; set; }

        [CINamedArgument("/B2")]
        [CIArgumentBoundary("10", "40")]
        [CIExclusiveArgument(1)]
        public byte MyByte2 { get; set; }

        [CINamedArgument("/SB")]
        public sbyte MySByte { get; set; }

        [CINamedArgument("/C1")]
        [CIArgumentBoundary("a", "z")]
        public char MyChar{ get; set; }

        [CIShortcutArgument("/INTEGER16", "/I16")]
        public short MyShort1 { get; set; }
        
        [CINamedArgument("/I32")]
        public int MyInteger1 { get; set; }

        [CINamedArgument("/I64")]
        public long MyLong1 { get; set; }

        [CINamedArgument("/UI16")]
        public ushort MyUShort { get; set; }
        [CINamedArgument("/UI32")]
        public uint MyUInt { get; set; }
        [CINamedArgument("/UI64")]
        public ulong MyULong { get; set; }

        [CINamedArgument("/DEC")]
        public decimal MyDecimal { get; set; }

        [CINamedArgument("/SINGLE")]
        public float MySingle { get; set; }

        [CINamedArgument("/D")]
        public double MyDouble1 { get; set; }
        
        [CINamedArgument("/S")]
        [CIArgumentValueLength(12, 20)]
        public string MyString1 { get; set; }

        // Email address regex
        [CINamedArgument("/EMAIL")]
        [CIArgumentFormat(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$")]
        public string EmailAddress { get; set; }

        [CINamedArgument("/STARTDATE")]
        public DateTime StartDate { get; set; }

        [CINamedArgument("/URI")]
        public Uri MyUri { get; set; }

        [CINamedArgument("/VERSION")]
        public Version MyVersion { get; set; }

        [CINamedArgument("/CHARARRAY")]
        public char[] CharArray { get; set; }

        [CINamedArgument("/BYTEARRAY")]
        public byte[] ByteArray { get; set; }
    }
}
