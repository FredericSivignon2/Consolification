using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Specifies that the related argument is mandatory.
    /// </summary>
    /// <remarks>
    /// If a mandatory argument is missing, the associated IJob won't be executed and the
    /// ConsolificationEngine.Start() method won't return 0 by default.
    /// Moreover, a mandatory argument that is a child of a parent argument, that is
    /// itself not mandatory, is not considered as mandatory if its parent is not 
    /// present in the command given arguments.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class CIMandatoryArgumentAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the <cref>Consolification.Core.Attributes.CIMandatoryArgumentAttribute</cref> class.
        /// </summary>
        public CIMandatoryArgumentAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <cref>Consolification.Core.Attributes.CIMandatoryArgumentAttribute</cref> class
        /// with specific prompt information.
        /// </summary>
        /// <param name="promptUser">A boolean value that indicates whether the user must be prompted if the correpsonding
        /// mandatory argument is not present.</param>
        /// <param name="promptMessage">A string that contains the message to display when prompting the user.</param>
        public CIMandatoryArgumentAttribute(bool promptUser, string promptMessage)
        {
            PromptUser = promptUser;
            PromptMessage = promptMessage;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a boolean value that indicates whether the user must be prompted if the correpsonding
        /// mandatory argument is not present.
        /// </summary>
        public bool PromptUser { get; private set; }
        /// <summary>
        /// Gets a string that contains the message to display when prompting the user.
        /// </summary>
        public string PromptMessage { get; private set; }

        #endregion
    }
}
