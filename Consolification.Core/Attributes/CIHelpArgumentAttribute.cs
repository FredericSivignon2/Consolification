using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CIHelpArgumentAttribute : CIArgumentAttribute
    {
        public CIHelpArgumentAttribute(string name) : base(name)
        {
        }

        public CIHelpArgumentAttribute(string name, string description) : base(name, description)
        {
        }

        public CIHelpArgumentAttribute(string[] names) : base(names)
        {
        }

        public CIHelpArgumentAttribute(string[] names, string description) : base(names, description)
        {
        }
    }
}
