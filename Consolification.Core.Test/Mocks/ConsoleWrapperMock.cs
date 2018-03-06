using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
	class ConsoleWrapperMock : IConsoleWrapper
	{
		private string password;
		private int index = 0;

		public string Output { get; private set; }

		public ConsoleWrapperMock(string password)
		{
			this.password = password;
		}

        public ConsoleKeyInfo ReadKey()
        {
            return ReadKey(false);
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
		{
			char[] array = password.ToCharArray();
			if (index >= array.Length)
				return new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false);
			return new ConsoleKeyInfo(array[index++], ConsoleKey.V, false, false, false);
		}

        public string ReadLine()
        {
            return "";
        }

		public void Write(char value)
		{
			Output += value;
		}

		public void Write(string value)
		{
			Output += value;
		}

        public void Write(string format, object arg0)
        {
            
        }

        public void Write(string format, object arg0, object arg1)
        {
            
        }

        public void Write(string format, object arg0, object arg1, object arg2)
        {
            
        }

        public void Write(string format, object[] args)
        {
            
        }

        public void WriteLine(string value)
        {
            Output += value;
        }

        public void WriteLine(string format, object arg0)
        {
            
        }

        public void WriteLine(string format, object arg0, object arg1)
        {
            
        }

        public void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            
        }

        public void WriteLine(string format, object[] args)
        {
            
        }
    }
}
