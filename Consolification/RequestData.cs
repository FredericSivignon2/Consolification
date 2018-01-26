using Consolification.Core;
using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification
{
    public class RequestData : ArgumentsContainer
    {
        [CIArgument("-url", "The URL of the request to perform.")]
        [CIMandatoryArgument]
        [CIJob(typeof(RequestJob))]
        public string URL { get; private set; }
    }
}
