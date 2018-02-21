using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CIArgumentAttribute : Attribute
    {
        #region Constructors
        public CIArgumentAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            this.Name = this.NamesString = name;
            this.Names = new string[] { name };
            this.NamesLength = name.Length;
        }

        public CIArgumentAttribute(string name, string description) : this(name)
        {
            this.Description = description;
        }
        
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the argument name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the argument description
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// If the argument has got several name (a 'main' name and a shortcut for example),
        /// gets an array of all those names.
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
