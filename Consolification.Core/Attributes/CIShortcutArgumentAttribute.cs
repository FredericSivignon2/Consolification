using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIShortcutArgumentAttribute : CIArgumentAttribute
    {
        #region Constructors
        public CIShortcutArgumentAttribute(string name, string shortcut)
            : base(name)
        {
            if (string.IsNullOrWhiteSpace(shortcut))
                throw new ArgumentNullException("shortcut");
            Initialize(name, shortcut);
            
        }

        public CIShortcutArgumentAttribute(string name, string shortcut, string description) : base(name, description)
        {
            if (string.IsNullOrWhiteSpace(shortcut))
                throw new ArgumentNullException("shortcut");
            Initialize(name, shortcut);
        }

        #endregion

        #region Public Properties
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
