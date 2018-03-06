﻿using Consolification.Core;
using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Consolification
{
    [CIHelpArgument("/?")]
    [CICommandDescription("Performs an HTTP request and get some result statistics.")]
    public class RequestData
    {
        [CIArgument("/url", "The URL of the request to perform.")]
        [CIMandatoryArgument]
        [CIJob(typeof(RequestJob))]
        public Uri URL { get; private set; }

        [CIShortcutArgument("/user", "/u", "The user to authenticate the request.")]
        [CIParentArgument(1)]
        public string User { get; private set; }

        [CIShortcutArgument("/password", "/p", "This user password to authenticate the request.")]
        [CIChildArgument(1)]
        [CIMandatoryArgument(true)]
        public string Password { get; private set; }
    }
}
