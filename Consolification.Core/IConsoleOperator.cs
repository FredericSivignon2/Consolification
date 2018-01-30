using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public interface IConsoleOperator
    {
        void Write(string message);
        void WriteLine(string message);
        //void WriteFormat(string message, );

        void WriteDebug(string message);
        void WriteInfo(string message);
        void WriteWarn(string message);
        void WriteError(string message);
        void WriteFatal(string message);

        SecureString GetPassword();
    }
}
