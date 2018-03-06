using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public interface IPasswordReader
    {
        string GetPassword();
        string GetPassword(char passwordChar);
        SecureString GetSecurePassword();
        SecureString GetSecurePassword(char passwordChar);
    }
}
