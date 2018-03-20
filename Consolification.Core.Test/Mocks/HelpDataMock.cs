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
    public class HelpDataMock
    {
        [CISimpleArgument("source", "The source file path to copy.")]
        [CIMandatoryArgument]
        public string Source { get; private set; }

        [CISimpleArgument("destination", "The destination path.")]
        public string Destination { get; private set; }

        [CINamedArgument("-format", "This is the format parameter.", "formatValue")]
        public string Data1 { get; private set; }

        [CINamedArgument("-method", "This is the method parameter.", "methodValue")]
        [CIMandatoryArgument()]
        public int Data2 { get; private set; }
    }
}
