using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class Log4NetWriter : ILogWriter
    {
        #region Data Members
        private readonly ILog log;
        #endregion

        #region Constructors
        public Log4NetWriter(string name)
        {
            log = LogManager.GetLogger(name);
        }
        #endregion

        #region ILogWriter 
        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Debug(string message, Exception exp)
        {
            log.Debug(message, exp);
        }

        public void DebugFormat(string message, object arg0)
        {
            log.DebugFormat(message, arg0);
        }

        public void DebugFormat(string message, object arg0, object arg1)
        {
            log.DebugFormat(message, arg0, arg1);
        }

        public void DebugFormat(string message, object arg0, object arg1, object arg2)
        {
            log.DebugFormat(message, arg0, arg1, arg2);
        }

        public void DebugFormat(string message, object[] args)
        {
            log.DebugFormat(message, args);
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public void Error(string message, Exception exp)
        {
            log.Error(message, exp);
        }

        
        public void ErrorFormat(string message, object arg0)
        {
            log.ErrorFormat(message, arg0);
        }

        public void ErrorFormat(string message, object arg0, object arg1)
        {
            log.ErrorFormat(message, arg0, arg1);
        }

        public void ErrorFormat(string message, object arg0, object arg1, object arg2)
        {
            log.ErrorFormat(message, arg0, arg1, arg2);
        }

        public void ErrorFormat(string message, object[] args)
        {
            log.ErrorFormat(message, args);
        }

        public void Fatal(string message)
        {
            log.Fatal(message);
        }

        public void Fatal(string message, Exception exp)
        {
            log.Fatal(message, exp);
        }
        
        public void FatalFormat(string message, object arg0)
        {
            log.FatalFormat(message, arg0);
        }

        public void FatalFormat(string message, object arg0, object arg1)
        {
            log.FatalFormat(message, arg0, arg1);
        }

        public void FatalFormat(string message, object arg0, object arg1, object arg2)
        {
            log.FatalFormat(message, arg0, arg1, arg2);
        }

        public void FatalFormat(string message, object[] args)
        {
            log.FatalFormat(message, argss);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Info(string message, Exception exp)
        {
            log.Info(message, exp);
        }

        public void InfoFormat(string message, object arg0)
        {
            log.InfoFormat(message, arg0);
        }

        public void InfoFormat(string message, object arg0, object arg1)
        {
            log.InfoFormat(message, arg0, arg1);
        }

        public void InfoFormat(string message, object arg0, object arg1, object arg2)
        {
            log.InfoFormat(message, arg0, arg1, arg2);
        }

        public void InfoFormat(string message, object[] args)
        {
            log.InfoFormat(message, args);
        }

        public void Warn(string message)
        {
            log.Warn(message);
        }

        public void Warn(string message, Exception exp)
        {
            log.Warn(message, exp);
        }
        
        public void WarnFormat(string message, object arg0)
        {
            log.WarnFormat(message, arg0);
        }

        public void WarnFormat(string message, object arg0, object arg1)
        {
            log.WarnFormat(message, arg0, arg1);
        }

        public void WarnFormat(string message, object arg0, object arg1, object arg2)
        {
             log.WarnFormat(message, arg0, arg1, arg2);
        }

        public void WarnFormat(string message, object[] args)
        {
            log.WarnFormat(message, args);
        }
        #endregion
    }
}
