using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    public class CIArgumentBoundaryAttribute : Attribute
    {
        public string MinValue { get; private set; } = null;
        public string MaxValue { get; private set; } = null;

        public CIArgumentBoundaryAttribute(string minValue)
        {
            this.MinValue = minValue;
        }

        public CIArgumentBoundaryAttribute(string minValue, string maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}
