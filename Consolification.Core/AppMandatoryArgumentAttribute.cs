using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class AppMandatoryArgumentAttribute : Attribute
    {
        public bool PromptUser { get; private set; }
        public bool Password { get; private set; }
        public char PasswordChar { get; private set; } = '*';

        public AppMandatoryArgumentAttribute()
        {
        }

        public AppMandatoryArgumentAttribute(bool promptUser)
        {
            PromptUser = promptUser;
        }

        public AppMandatoryArgumentAttribute(bool promptUser, bool password)
        {
            PromptUser = promptUser;
            Password = password;
        }

        public AppMandatoryArgumentAttribute(bool promptUser, bool password, char passwordChar)
        {
            PromptUser = promptUser;
            Password = password;
            PasswordChar = passwordChar;
        }
    }
}
