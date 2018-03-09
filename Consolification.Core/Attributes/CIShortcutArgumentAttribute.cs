using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Similar to the <cref>Consolification.Core.Attributes.CIShortcutArgumentAttribute</cref> attribute,
    /// but also allow the user to specify a shortcut name for the correpsonding argument.
    /// </summary>
    /// <remarks>
    /// This attribute can be usefull if you want for example define an explicit argument name  like 
    /// "/password", "--cleanup"...  You can specify a shortcut name for those arguments, like
    /// "/p", "--c"... Then, you can use one or the other within your command line.    /// 
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class CIShortcutArgumentAttribute : CINamedArgumentAttribute
    {
        #region Constructors
        /// <summary>
        /// Initiliazes a new instance of the <cref>Consolification.Core.Attributes.CIShortcutArgumentAttribute</cref> class
        /// with the given name and shortcut.
        /// </summary>
        /// <param name="name">The name of the argument corresponding to the associated property (aka 'long name').</param>
        /// <param name="shortcut">The shortcut name of the argument.</param>
        public CIShortcutArgumentAttribute(string name, string shortcut)
            : base(name)
        {
            if (string.IsNullOrWhiteSpace(shortcut))
                throw new ArgumentNullException("shortcut");
            Initialize(name, shortcut);
            
        }

        /// <summary>
        /// Initiliazes a new instance of the <cref>Consolification.Core.Attributes.CIShortcutArgumentAttribute</cref> class
        /// with the given name, shortcut and description.
        /// </summary>
        /// <param name="name">The name of the argument corresponding to the associated property (aka 'long name').</param>
        /// <param name="shortcut">The shortcut name of the argument.</param>
        /// <param name="description">The description of the argument corresponding to the associated property. It will appear in the
        /// auto generatd help.</param>
        public CIShortcutArgumentAttribute(string name, string shortcut, string description) : base(name, description)
        {
            if (string.IsNullOrWhiteSpace(shortcut))
                throw new ArgumentNullException("shortcut");
            Initialize(name, shortcut);
        }

        /// <summary>
        /// Initiliazes a new instance of the <cref>Consolification.Core.Attributes.CIShortcutArgumentAttribute</cref> class
        /// with the given name, shortcut and description.
        /// </summary>
        /// <param name="name">The name of the argument corresponding to the associated property (aka 'long name').</param>
        /// <param name="shortcut">The shortcut name of the argument.</param>
        /// <param name="description">The description of the argument corresponding to the associated property. It will appear in the
        /// auto generatd help.</param>
        public CIShortcutArgumentAttribute(string name, string shortcut, string description, string valueHelpText) : base(name, description, valueHelpText)
        {
            if (string.IsNullOrWhiteSpace(shortcut))
                throw new ArgumentNullException("shortcut");
            Initialize(name, shortcut);
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the shortcut name of the corresponding argument.
        /// </summary>
        public string Shortcut { get; set; }
        #endregion

        #region Private Methods
        private void Initialize(string name, string shortcut)
        {
            this.Shortcut = shortcut;
            this.Names = new string[] { name, shortcut };
            this.NamesString = name + ", " + shortcut;
            this.NamesLength = this.NamesString.Length;
        }
        #endregion
    }
}
