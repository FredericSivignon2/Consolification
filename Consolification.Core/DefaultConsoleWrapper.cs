using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
	public class DefaultConsoleWrapper : IConsoleWrapper
	{
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return Console.ReadKey(intercept);
		}

		public void Write(char value)
		{
			Console.Write(value);
		}

		public void Write(string value)
		{
			Console.Write(value);
		}
	}
}
