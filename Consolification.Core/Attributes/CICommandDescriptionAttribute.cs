using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Provides a description of the related command. This description is used within the
    /// auto generated help to provide a summary of what the related command does.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CICommandDescriptionAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <cref>CICommandDescriptionAttribute</cref> class with
        /// the given description.
        /// </summary>
        /// <param name="description">The description to associate with the related command.</param>
        public CICommandDescriptionAttribute(string description)
        {
            this.Description = description;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; }
        #endregion
    }
}
