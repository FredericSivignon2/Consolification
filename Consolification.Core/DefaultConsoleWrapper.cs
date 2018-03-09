using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class DefaultConsoleWrapper : IConsoleWrapper
    {
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(char value)
        {
            Console.Write(value);
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void Write(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
        }

        public void Write(string format, object arg0, object arg1)
        {
            Console.WriteLine(format, arg0, arg1);
        }

        public void Write(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(format, arg0, arg1, arg2);
        }
        
        public void Write(string format, object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
        }

        public void WriteLine(string format, object arg0, object arg1)
        {
            Console.WriteLine(format, arg0, arg1);
        }

        public void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(format, arg0, arg1, arg2);
        }

        public void WriteLine(string format, object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WriteLine(string value, Exception exp)
        {
            Console.WriteLine(value);
            Console.WriteLine(exp.FullMessage());
        }
    }
}
