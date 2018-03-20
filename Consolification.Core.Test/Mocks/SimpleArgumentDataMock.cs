using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class SimpleArgumentDataMock
    {
        [CISimpleArgument]
        public string Source { get; private set; }

        [CISimpleArgument]
        public string Destination { get; private set; }
    }
}
