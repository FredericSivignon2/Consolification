using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class DataJobMock : ArgumentsContainer
    {
        [CIArgument("/A")]
        [CIJob(typeof(JobMock))]
        public string In { get; private set; }

        public string Out { get; set; }
    }
}
