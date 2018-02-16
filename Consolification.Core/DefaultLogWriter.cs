using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class DefaultLogWriter : ILogWriter
    { 
        public enum Level
        {
            Debug = 0,
            Info = 1,
            Warn = 2,
            Error = 3,
            Fatal = 4
        };

        #region Constructors
        public DefaultLogWriter()
        {
            WriteToOutput = (message) =>
            {
                Console.WriteLine(message);
            };
        }

        public DefaultLogWriter(Action<string> writeToOutput)
        {
            WriteToOutput = writeToOutput;
        }
        #endregion

        #region Public Properties
        public Action<string> WriteToOutput;
        public Level LogLevel { get; set; } = DefaultLogWriter.Level.Debug;
        public bool ShowLevelPrefix { get; set; } = false;
        public string PrevixSeparator { get; set; } = " - ";
        #endregion

        #region Public Methods
        public void Debug(string message)
        {
            WriteMessage(Level.Debug, message);
        }

        public void Debug(string message, Exception exp)
        {
            WriteMessage(Level.Debug, message, exp);
        }

        public void DebugFormat(string message, object arg0)
        {
            WriteMessageFormat(Level.Debug, message, arg0);
        }

        public void DebugFormat(string message, object arg0, object arg1)
        {
            WriteMessageFormat(Level.Debug, message, arg0, arg1);
        }

        public void DebugFormat(string message, object arg0, object arg1, object arg2)
        {
            WriteMessageFormat(Level.Debug, message, arg0, arg1, arg2);
        }

        public void DebugFormat(string message, object[] args)
        {
            WriteMessageFormat(Level.Debug, message, args);
        }

        public void Error(string message)
        {
            WriteMessage(Level.Error, message);
        }

        public void Error(string message, Exception exp)
        {
            WriteMessage(Level.Error, message, exp);
        }

        public void ErrorFormat(string message, object arg0)
        {
            WriteMessageFormat(Level.Error, message, arg0);
        }

        public void ErrorFormat(string message, object arg0, object arg1)
        {
            WriteMessageFormat(Level.Error, message, arg0, arg1);
        }

        public void ErrorFormat(string message, object arg0, object arg1, object arg2)
        {
            WriteMessageFormat(Level.Error, message, arg0, arg1, arg2);
        }

        public void ErrorFormat(string message, object[] args)
        {
            WriteMessageFormat(Level.Error, message, args);
        }

        public void Fatal(string message)
        {
            WriteMessage(Level.Fatal, message);
        }

        public void Fatal(string message, Exception exp)
        {
            WriteMessage(Level.Fatal, message, exp);
        }

        public void FatalFormat(string message, object arg0)
        {
            WriteMessageFormat(Level.Fatal, message, arg0);
        }

        public void FatalFormat(string message, object arg0, object arg1)
        {
            WriteMessageFormat(Level.Fatal, message, arg0, arg1);
        }

        public void FatalFormat(string message, object arg0, object arg1, object arg2)
        {
            WriteMessageFormat(Level.Fatal, message, arg0, arg1, arg2);
        }

        public void FatalFormat(string message, object[] args)
        {
            WriteMessageFormat(Level.Fatal, message, args);
        }

        public void Info(string message)
        {
            WriteMessage(Level.Info, message);
        }

        public void Info(string message, Exception exp)
        {
            WriteMessage(Level.Info, message, exp);
        }

        public void InfoFormat(string message, object arg0)
        {
            WriteMessageFormat(Level.Info, message, arg0);
        }

        public void InfoFormat(string message, object arg0, object arg1)
        {
            WriteMessageFormat(Level.Info, message, arg0, arg1);
        }

        public void InfoFormat(string message, object arg0, object arg1, object arg2)
        {
            WriteMessageFormat(Level.Info, message, arg0, arg1, arg2);
        }

        public void InfoFormat(string message, object[] args)
        {
            WriteMessageFormat(Level.Info, message, args);
        }

        public void Warn(string message)
        {
            WriteMessage(Level.Warn, message);
        }

        public void Warn(string message, Exception exp)
        {
            WriteMessage(Level.Warn, message, exp);
        }

        public void WarnFormat(string message, object arg0)
        {
            WriteMessageFormat(Level.Warn, message, arg0);
        }

        public void WarnFormat(string message, object arg0, object arg1)
        {
            WriteMessageFormat(Level.Warn, message, arg0, arg1);
        }

        public void WarnFormat(string message, object arg0, object arg1, object arg2)
        {
            WriteMessageFormat(Level.Warn, message, arg0, arg1, arg2);
        }

        public void WarnFormat(string message, object[] args)
        {
            WriteMessageFormat(Level.Warn, message, args);
        }
        #endregion

        #region Private Methods
        private void WriteMessage(Level level, string message)
        {
            WriteMessage(level, message, null);
        }

        private void WriteMessage(Level level, string message, Exception exp)
        {
            if (NeedToWriteMessage(level) == false)
                return;

            StringBuilder builder = new StringBuilder(GetPrefix(level));
            builder.Append(message);
            while (exp != null)
            {
                builder.AppendLine();
                builder.Append(exp.Message);
                exp = exp.InnerException;
            }
            WriteToOutput(builder.ToString());
        }

        private void WriteMessageFormat(Level level, string message, object arg0)
        {
            if (NeedToWriteMessage(level) == false)
                return;

            StringBuilder builder = new StringBuilder(GetPrefix(level));
            builder.AppendFormat(message, arg0);

            WriteToOutput(builder.ToString());
        }

        private void WriteMessageFormat(Level level, string message, object arg0, object arg1)
        {
            if (NeedToWriteMessage(level) == false)
                return;

            StringBuilder builder = new StringBuilder(GetPrefix(level));
            builder.AppendFormat(message, arg0, arg1);

            WriteToOutput(builder.ToString());
        }

        private void WriteMessageFormat(Level level, string message, object arg0, object arg1, object arg2)
        {
            if (NeedToWriteMessage(level) == false)
                return;

            StringBuilder builder = new StringBuilder(GetPrefix(level));
            builder.AppendFormat(message, arg0, arg1, arg2);

            WriteToOutput(builder.ToString());
        }

        private void WriteMessageFormat(Level level, string message, object[] args)
        {
            if (NeedToWriteMessage(level) == false)
                return;

            StringBuilder builder = new StringBuilder(GetPrefix(level));
            builder.AppendFormat(message, args);

            WriteToOutput(builder.ToString());
        }

        private string GetPrefix(Level level)
        {
            if (ShowLevelPrefix)
            {
                return level.ToString().ToUpper() + PrevixSeparator;
            }
            return string.Empty;            
        }

        private bool NeedToWriteMessage(Level level)
        {
            return level >= LogLevel;
        }
        
        #endregion
    }
}
