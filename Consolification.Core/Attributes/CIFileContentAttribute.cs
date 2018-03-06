using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIFileContentAttribute : Attribute
    {
        #region Constructors
        public CIFileContentAttribute()
        {
            Encoding = Encoding.Default;
        }

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
        public Encoding Encoding { get; private set; }
        #endregion
    }
}
