﻿using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class AllDataTypeMock
    {
        [CINamedArgument("/A")]
        public bool MyBoolean1 { get; set; }

        [CINamedArgument("/B")]
        public byte MyByte1 { get; set; }

        [CINamedArgument("/B2")]
        [CIArgumentBoundary("10", "40")]
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
        public string MyString1 { get; set; }

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
