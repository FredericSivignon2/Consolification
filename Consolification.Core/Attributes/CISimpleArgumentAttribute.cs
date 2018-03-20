using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Defines a simple argument (meaning that there is no name associated with this argument.
    /// Think about the 'SOURCE' and 'DESTINATION' arguments in the DOS 'COPY' command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CISimpleArgumentAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <cref>CISimpleArgumentAttribute</cref> class with the specified 
        /// argument position index.
        /// </summary>
        public CISimpleArgumentAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <cref>CISimpleArgumentAttribute</cref> class with the specified 
        /// argument position index, help text and description.
        /// </summary>
        /// <param name="helpText"></param>
        /// <param name="description"></param>
        public CISimpleArgumentAttribute(string helpText, string description)
        {
            if (string.IsNullOrWhiteSpace(helpText))
                throw new ArgumentNullException("helpText");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("description");

            HelpText = helpText;
            Description = description;
        }
        #endregion

        #region Public Properties
        public string HelpText { get; private set; }
        public string Description { get; private set; }
        #endregion
    }
}
