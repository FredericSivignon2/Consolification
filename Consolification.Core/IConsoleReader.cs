using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public interface IConsoleReader
    {
        SecureString GetPassword();
    }
}
