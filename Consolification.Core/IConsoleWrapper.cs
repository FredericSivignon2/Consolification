using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
	public interface IConsoleWrapper
	{
        ConsoleKeyInfo ReadKey();
        ConsoleKeyInfo ReadKey(bool intercept);
        string ReadLine();

		void Write(char value);
		void Write(string value);
        void Write(string format, object arg0);
        void Write(string format, object arg0, object arg1);
        void Write(string format, object arg0, object arg1, object arg2);
        void Write(string format, object[] args);
        void WriteLine(string value);
        void WriteLine(string format, object arg0);
        void WriteLine(string format, object arg0, object arg1);
        void WriteLine(string format, object arg0, object arg1, object arg2);
        void WriteLine(string format, object[] args);
        void WriteLine(string value, Exception exp);
    }
}
