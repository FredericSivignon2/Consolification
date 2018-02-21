using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    [CIHelpArgument("--help")]
    [CICommandDescription("This is a dummy class for test purpose.")]
    public class HelpDataMock : ArgumentsContainer
    {
        [CIArgument("-data1", "This is the data1 parameter.")]
        public string Data1 { get; private set; }

        [CIArgument("-data2", "This is the data2 parameter.")]
        [CIMandatoryArgument()]
        public int Data2 { get; private set; }
    }
}
