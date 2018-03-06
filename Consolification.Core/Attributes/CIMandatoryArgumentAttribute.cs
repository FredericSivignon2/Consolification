using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIMandatoryArgumentAttribute : Attribute
    {
        public bool PromptUser { get; private set; }
        public string PromptMessage { get; private set; }
        
        public CIMandatoryArgumentAttribute()
        {
        }

        public CIMandatoryArgumentAttribute(bool promptUser, string promptMessage)
        {
            PromptUser = promptUser;
            PromptMessage = promptMessage;
        }
    }
}
