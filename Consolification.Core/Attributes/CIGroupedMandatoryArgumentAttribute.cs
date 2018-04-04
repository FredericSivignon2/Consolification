using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIGroupedMandatoryArgumentAttribute : Attribute
    {
        #region Constructors
        
        public CIGroupedMandatoryArgumentAttribute(int groupId)
        {
            if (groupId < 1)
                throw new ArgumentException("The groupId cannot be lower than 1.");
            this.GroupId = groupId;
        }
        #endregion

        public int GroupId { get; private set; }
    }
}
