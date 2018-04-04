using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Ensure that other arguments at the same level cannot be specified if the
    /// associated argument is itself specified in the command line.
    /// </summary>
    /// <remarks>
    /// By default, the GroupId property is set to 0, meaning that the associated
    /// argument is exclusive to all other arguments at the same level.
    /// If you specify a specific GroupId, it will be exclusive only for other
    /// argument for which a CIExclusiveArgumentAttribute attribute is specified
    /// with the same GroupId.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class CIExclusiveArgumentAttribute : Attribute
    {
        public CIExclusiveArgumentAttribute()
        {
            GroupId = 0;
        }

        public CIExclusiveArgumentAttribute(int groupId)
        {
            GroupId = groupId;
        }

        public int GroupId { get; private set; }
    }
}
