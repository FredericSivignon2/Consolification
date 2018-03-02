using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
	public interface IConsoleWrapper
	{
		ConsoleKeyInfo ReadKey(bool intercept);
		void Write(char value);
		void Write(string value);
	}
}
