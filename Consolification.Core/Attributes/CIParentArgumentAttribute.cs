using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Specifies the corresponding argument is a parent argument.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CIParentArgumentAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <cref>Consolification.Core.Attributes.CIParentArgumentAttribute</cref> class
        /// with the given identifier.
        /// </summary>
        /// <param name="id">The identifier of this parent argument.</param>
        public CIParentArgumentAttribute(int id)
        {
            if (id < 1)
                throw new ArgumentException("The argument id must be equal or greater than 1.");
            this.Id = id;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the parent identifier.
        /// </summary>
        public int Id { get; private set; }
        #endregion
    }
}
