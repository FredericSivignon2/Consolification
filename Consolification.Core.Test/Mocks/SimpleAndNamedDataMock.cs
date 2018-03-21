using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class SimpleAndNamesDataMock
    {
        [CISimpleArgument]
        public string Source { get; private set; }
        [CINamedArgument("--max")]
        public int Max { get; private set; }
    }
}
