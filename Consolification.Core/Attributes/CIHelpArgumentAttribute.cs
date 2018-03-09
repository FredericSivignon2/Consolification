using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Specifies the name of the argument used to display the command help.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CIHelpArgumentAttribute : CINamedArgumentAttribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <cref>CIHelpArgumentAttribute</cref> class with
        /// the given argument name.
        /// </summary>
        /// <param name="name">The name of the help argument to provide to the command.</param>
        public CIHelpArgumentAttribute(string name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <cref>CIHelpArgumentAttribute</cref> class with
        /// the given argument name and description.
        /// </summary>
        /// <param name="name">The name of the help argument to provide to the command.</param>
        /// <param name="description">The description of the command to display when the user
        /// specifies the help command.</param>
        public CIHelpArgumentAttribute(string name, string description) : base(name, description)
        {
        }
        #endregion
    }
}
