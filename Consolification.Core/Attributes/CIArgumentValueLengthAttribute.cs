using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIArgumentValueLengthAttribute : Attribute
    {
        public CIArgumentValueLengthAttribute(int maxLength)
        {
            if (maxLength <= 0)
                throw new ArgumentException("maxLength must be greater than 0.");
            MaxLength = maxLength;
        }

        public CIArgumentValueLengthAttribute(int minLength, int maxLength)
        {
            if (minLength < 0)
                throw new ArgumentException("minLength must be greater or equal to 0.");
            if (maxLength < minLength)
                throw new ArgumentException("maxLength must be equal or greater than minLength.");

            MinLength = minLength;
            MaxLength = maxLength;
        }

        public int MinLength { get; private set; } = 0;
        public int MaxLength { get; private set; } = 0;
    }
}
