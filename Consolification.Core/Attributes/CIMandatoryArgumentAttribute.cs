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
        public bool Password { get; private set; }
        public char PasswordChar { get; private set; } = '*';

        public CIMandatoryArgumentAttribute()
        {
        }

        public CIMandatoryArgumentAttribute(bool promptUser)
        {
            PromptUser = promptUser;
        }

        public CIMandatoryArgumentAttribute(bool promptUser, bool password)
        {
            PromptUser = promptUser;
            Password = password;
        }

        public CIMandatoryArgumentAttribute(bool promptUser, bool password, char passwordChar)
        {
            PromptUser = promptUser;
            Password = password;
            PasswordChar = passwordChar;
        }
    }
}
