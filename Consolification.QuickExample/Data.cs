using Consolification.Core.Attributes;
using System;

namespace Consolification.QuickExample
{
    [CIHelpArgument("/?")]
    [CIJob(typeof(MessageJob))]
    public class Data
    {
        [CISimpleArgument("message", "The message to display.")]
        [CIMandatoryArgument]
        public string Message { get; private set; }

        [CINamedArgument("--v", "This is a value.", "val")]
        [CIArgumentBoundary("5", "20")]
        public int Value { get; private set; }
    }
}
