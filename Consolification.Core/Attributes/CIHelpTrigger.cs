using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CIHelpTrigger : Attribute
    {
        public string Name { get; set; }

        public CIHelpTrigger(string name)
        {
        }
    }
}
