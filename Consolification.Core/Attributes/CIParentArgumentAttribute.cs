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

        public CIParentArgumentAttribute(int id, int parentId)
        {
            if (id < 1)
                throw new ArgumentException("The argument id must be equal or greater than 1.");
            if (parentId < 1)
                throw new ArgumentException("The argument parentId must be equal or greater than 1.");
            this.Id = id;
            this.ParentId = parentId;
        }

        public int Id { get; private set; }
        public int ParentId { get; private set; }
    }
}
