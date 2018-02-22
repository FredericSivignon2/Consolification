using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIParentArgumentAttribute : Attribute
    {
        public CIParentArgumentAttribute(int id)
        {
            if (id < 1)
                throw new ArgumentException("The argument id must be equal or greater than 1.");
            this.Id = id;
        }
        
        public int Id { get; private set; }
    }
}
