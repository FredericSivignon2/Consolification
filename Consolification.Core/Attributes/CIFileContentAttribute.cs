using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Specifies that argument value is a path of a file for which content must be automatically loaded
    /// into the corresponding property value.
    /// </summary>
    /// <remarks>
    /// This attribute can be associated with the following property types:
    /// 1) <cref>System.String</cref>: The entire file content is put into a string.
    /// 2) <cref>System.String[]</cref>: Each file line (only relevant for text file) is a string of the resulting array.
    /// 3) <cref>System.Byte[]</cref>: The entire file content is put into a byte array.
    /// 4) <cref>System.Char[]</cref>: The entire file content is put into a char array.
    /// 5) <cref>System.IO.FileStream</cref>: An FileStream stream is opened from the corresponding file.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class CIFileContentAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the <cref>CIFileContentAttribute</cref> class. 
        /// </summary>
        public CIFileContentAttribute()
        {
            Encoding = Encoding.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <cref>CIFileContentAttribute</cref> class with
        /// the given character encoding.
        /// </summary>
        /// <param name="encoding">The type of encoding to use to read the file.</param>
        public CIFileContentAttribute(string encoding)
        {
            switch (encoding.ToUpper())
            {
                case "ASCII": Encoding = Encoding.ASCII; break;
                case "UNICODE": Encoding = Encoding.Unicode; break;
                case "UTF32": Encoding = Encoding.UTF32; break;
                case "UTF7": Encoding = Encoding.UTF7; break;
                case "UTF8": Encoding = Encoding.UTF8; break;
                default:
                    throw new ArgumentException("Invalid encoding value. Supported values are: ASCII, Unicode, UTF32, UTF7 and UTF8.");
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the encoding used to read the file.
        /// </summary>
        public Encoding Encoding { get; private set; }
        #endregion
    }
}
