using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Specifies that the associated argument is a child argument, meaning that it
    /// can be specified only if a 'parent' argument is also specified.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CIChildArgumentAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <cref>CIChildArgumentAttribute</cref> class with
        /// the specifid parent identifier.
        /// </summary>
        /// <param name="parentId">The identifier of the 'parent' argument (The property 
        /// associated with the parent argument must have the <cref>CIParentArgumentAttribute</cref>).</param>
        public CIChildArgumentAttribute(int parentId)
        {
            this.ParentId = parentId;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the identifier of the parent argument.
        /// </summary>
        public int ParentId { get; private set; }
        #endregion
    }
}
