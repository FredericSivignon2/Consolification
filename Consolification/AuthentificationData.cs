using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification
{
    public class AuthentificationData
    {
        [CIShortcutArgument("/user", "/u")]
        public string UserName { get; private set; }
    }
}
