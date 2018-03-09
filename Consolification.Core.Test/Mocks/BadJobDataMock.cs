using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    // FileDataMock does not implement IJob. So it must generate an error
    [CIJob(typeof(FileDataMock))] 
    public class BadJobDataMock
    {
        [CINamedArgument("/A")]
        public string Value { get; private set; }
    }
}
