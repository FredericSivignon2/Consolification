using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Specifies the corresponding argument value is a password. In that case,
    /// if the user must be prompted to enter the value, all characters read will
    /// be replaced by a specific character ('*' by default) in the console display.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CIPasswordAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the <cref>Consolification.Core.Attributes.CIPasswordAttribute</cref> class.
        /// </summary>
        public CIPasswordAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <cref>Consolification.Core.Attributes.CIPasswordAttribute</cref> class
        /// with the given password character.
        /// </summary>
        public CIPasswordAttribute(char passwordChar)
        {
            PasswordChar = passwordChar;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the character used to hide the password entered by user.
        /// </summary>
        public char PasswordChar { get; private set; } = '*';
        #endregion
    }
}
