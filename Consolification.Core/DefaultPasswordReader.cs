using Consolification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class DefaultPasswordReader : IPasswordReader
    {
		private IConsoleWrapper console;

		public DefaultPasswordReader(IConsoleWrapper wrapper)
		{
			this.console = wrapper;
		}

        public IConsoleWrapper Console
        {
            get
            {
                return this.console;
            }
        }

		public SecureString GetSecurePassword()
        {
            return GetSecurePassword('*');
        }

        public SecureString GetSecurePassword(char passwordChar)
        {
            string pass = GetPassword(passwordChar);

            SecureString spass = new SecureString();
            foreach (char c in pass.ToCharArray())
            {
                spass.AppendChar(c);
            }

            return spass;
        }

        public string GetPassword()
        {
            return GetPassword('*');
        }

        public string GetPassword(char passwordChar)
        { 
            ConsoleKeyInfo key;
            string pass = string.Empty;

            do
            {
                key = this.console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
					this.console.Write(passwordChar);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
						this.console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            return pass;
        }
    }
}
