using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CICommandDescriptionAttribute : Attribute
    {
        public CICommandDescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description { get; private set; }
    }
}
