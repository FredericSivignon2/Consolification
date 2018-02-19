﻿using System;
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

            this.Names = new string[] { name };
        }

        public CIArgumentAttribute(string name, string description) : this(name)
        {
            this.Description = description;
        }

        public CIArgumentAttribute(string[] names)
        {
            if (names == null)
                throw new ArgumentNullException("names");
            if (names.Length == 0)
                throw new ArgumentException("The given names array must contains at least one element.");

            this.Names = names;
        }

        public CIArgumentAttribute(string[] names, string description) : this(names)
        {
            this.Description = description;
        }
        #endregion

        #region Public Properties
        public string[] Names { get; private set; }
        public string Description { get; private set; }
        public int NamesLength
        {
            get
            {
                return NamesToString().Length;
            }
        }
        #endregion

        #region Public Methods
        public string NamesToString()
        {
            StringBuilder names = new StringBuilder();
            foreach (string name in this.Names)
            {
                if (names.Length > 0)
                    names.Append(", ");
                names.Append(name);
            }
            return names.ToString();
        }
        
        #endregion
    }
}
