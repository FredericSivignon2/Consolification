using Consolification.Core.Attributes;
using System;

namespace Consolification
{
    [CIHelpArgument("/?")]
    [CICommandDescription("Performs an HTTP request and get some result statistics.")]
    [CIJob(typeof(RequestJob))]
    public class RequestData
    {
        [CINamedArgument("/url", "The URL of the request to perform.", "value")]
        [CIMandatoryArgument]
        public Uri URL { get; private set; }

        [CIShortcutArgument("/user", "/u", "The user to authenticate the request.", "value")]
        public string User { get; private set; }

        [CIShortcutArgument("/password", "/p", "This user password to authenticate the request.", "value")]
        [CIMandatoryArgument(true, "User password: ")]
        [CIPassword]
        public string Password { get; private set; }
    }
}
