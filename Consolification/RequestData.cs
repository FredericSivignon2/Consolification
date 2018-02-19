using Consolification.Core;
using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification
{
    [CIHelpArgument("-help", "Display this help.")]
    public class RequestData : ArgumentsContainer
    {
        [CIArgument("-url", "The URL of the request to perform.")]
        [CIMandatoryArgument]
        [CIJob(typeof(RequestJob))]
        public string URL { get; private set; }

        [CIArgument(new string[] { "/tata", "/TATA" }, "This is the tata argument.")]
        public string Toto { get; set; }
    }
}
