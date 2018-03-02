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

		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			char[] array = password.ToCharArray();
			if (index >= array.Length)
				return new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false);
			return new ConsoleKeyInfo(array[index++], ConsoleKey.V, false, false, false);
		}

		public void Write(char value)
		{
			Output += value;
		}

		public void Write(string value)
		{
			Output += value;
		}
	}
}
