using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIPasswordAttribute : Attribute
    {
        public char PasswordChar { get; private set; } = '*';

        public CIPasswordAttribute()
        {
        }

        public CIPasswordAttribute(char passwordChar)
        {
            PasswordChar = passwordChar;
        }
    }
}
