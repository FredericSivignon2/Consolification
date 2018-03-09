using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Associates a Property with an argument name and description. 
    /// </summary>
    /// <remarks>
    /// Use this attribe for arguments that are composed of two strings: One string for the 
    /// argument name and one string for the argument value.
    /// Example: /URL http://www.google.fr
    /// 
    /// Where /URL is the argument name and http://www.google.fr the corresponding value.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CINamedArgumentAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <cref>CIArgumentAttribute</cref> class with the specified argument name.
        /// </summary>
        /// <param name="name">The name of the argument corresponding to the associated property.</param>
        public CINamedArgumentAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            this.Name = this.NamesString = name;
            this.Names = new string[] { name };
            this.NamesLength = name.Length;
        }

        /// <summary>
        /// Initializes a new instance of the <cref>CIArgumentAttribute</cref> class with the specified argument name and description.
        /// </summary>
        /// <param name="name">The name of the argument corresponding to the associated property.</param>
        /// <param name="description">The description of the argument corresponding to the associated property. It will appear in the
        /// auto generatd help.</param>
        public CINamedArgumentAttribute(string name, string description) : this(name)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(description);

            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <cref>CIArgumentAttribute</cref> class with the specified argument name and description.
        /// </summary>
        /// <param name="name">The name of the argument corresponding to the associated property.</param>
        /// <param name="description">The description of the argument corresponding to the associated property. It will appear in the
        /// auto generatd help.</param>
        /// <param name="valueHelpText"></param>
        public CINamedArgumentAttribute(string name, string description, string valueHelpText) : this(name, description)
        {
            if (string.IsNullOrWhiteSpace(valueHelpText))
                throw new ArgumentNullException(valueHelpText);

            this.ValueHelpText = valueHelpText;
        }        
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the argument name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the text displayed to designate the argument value. 
        /// </summary>
        public string ValueHelpText { get; private set; }
        /// <summary>
        /// Gets the argument description. This description will be use to build the auto generated command help.
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// If the argument has got several name (a 'main' name and a shortcut for example),
        /// gets an array of all those names (To be used in derived classes).
        /// </summary>
        public string[] Names { get; protected set; }
        /// <summary>
        /// Gets the length of the string that display all argument names.
        /// </summary>
        public int NamesLength { get; protected set; }
        /// <summary>
        /// Gets a string to display all argument names.
        /// </summary>
        public string NamesString { get; protected set; }
        #endregion
    }
}
