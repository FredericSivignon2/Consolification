using Consolification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class DefaultConsoleReader : IConsoleReader
    {
        public SecureString GetPassword()
        {
            return GetPassword('*');
        }

        public SecureString GetPassword(char passwordChar)
        {
            ConsoleKeyInfo key;
            string pass = "";

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write(passwordChar);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            SecureString spass = new SecureString();
            int index = 0;
            foreach (char c in pass.ToCharArray())
            {
                spass.SetAt(index++, c);
            }

            return spass;
        }
    }
}
