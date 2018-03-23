using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIArgumentFormatAttribute : Attribute
    {
        public CIArgumentFormatAttribute(string regex)
        {
            Regex = regex;
        }

        public CIArgumentFormatAttribute(string regex, RegexOptions options)
        {
            Regex = regex;
            Options = options;
        }

        public string Regex { get; private set; }

        public RegexOptions Options { get; private set; } = RegexOptions.None;
    }
}
