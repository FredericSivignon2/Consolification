using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIChildArgumentAttribute : Attribute
    {
        public CIChildArgumentAttribute(int parentId)
        {
            this.ParentId = parentId;
        }

        public int ParentId { get; private set; }
    }
}
