using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class SimpleDataMock : IArgumentsContainer
    {
        [AppArgument("/A")]
        public bool MyBoolean1 { get; set; }

        [AppArgument("/B")]
        public byte MyByte1 { get; set; }

        [AppArgument("/B2", "10", "40")]
        public byte MyByte2 { get; set; }

        [AppArgument("/I16")]
        public int MyShort1 { get; set; }
        
        [AppArgument("/I32")]
        public int MyInteger1 { get; set; }

        [AppArgument("/I64")]
        public long MyLong1 { get; set; }

        [AppArgument("/D")]
        public double MyDouble1 { get; set; }
        
        [AppArgument("/S")]
        public string MyString1 { get; set; }
    }
}
