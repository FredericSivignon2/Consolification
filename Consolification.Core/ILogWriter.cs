using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public interface ILogWriter
    {
        void Debug(string message);
        void Debug(string message, Exception exp);
        void DebugFormat(string message, object arg0);
        void DebugFormat(string message, object arg0, object arg1);
        void DebugFormat(string message, object arg0, object arg1, object arg2);
        void DebugFormat(string message, object[] args);

        void Info(string message);
        void Info(string message, Exception exp);
        void InfoFormat(string message, object arg0);
        void InfoFormat(string message, object arg0, object arg1);
        void InfoFormat(string message, object arg0, object arg1, object arg2);
        void InfoFormat(string message, object[] args);

        void Warn(string message);
        void Warn(string message, Exception exp);
        void WarnFormat(string message, object arg0);
        void WarnFormat(string message, object arg0, object arg1);
        void WarnFormat(string message, object arg0, object arg1, object arg2);
        void WarnFormat(string message, object[] args);

        void Error(string message);
        void Error(string message, Exception exp);
        void ErrorFormat(string message, object arg0);
        void ErrorFormat(string message, object arg0, object arg1);
        void ErrorFormat(string message, object arg0, object arg1, object arg2);
        void ErrorFormat(string message, object[] args);

        void Fatal(string message);
        void Fatal(string message, Exception exp);
        void FatalFormat(string message, object arg0);
        void FatalFormat(string message, object arg0, object arg1);
        void FatalFormat(string message, object arg0, object arg1, object arg2);
        void FatalFormat(string message, object[] args);
    }
}
