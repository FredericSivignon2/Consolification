using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class AppArgumentAttribute : Attribute
    {
        public string MinValue { get; private set; }
        public string MaxValue { get; private set; }
        public string[] Names { get; private set; }

        public AppArgumentAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            this.Names = new string[] { name };
        }

        public AppArgumentAttribute(string[] names)
        {
            if (names == null)
                throw new ArgumentNullException("names");
            if (names.Length == 0)
                throw new ArgumentException("The given names array must contains at least one element.");

            this.Names = names;
        }

        public AppArgumentAttribute(string name, string minValue)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            this.Names = new string[] { name };
            this.MinValue = minValue;
        }

        public AppArgumentAttribute(string name, string minValue, string maxValue)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            this.Names = new string[] { name };
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public AppArgumentAttribute(string[] names, string minValue)
        {
            if (names == null)
                throw new ArgumentNullException("names");
            if (names.Length == 0)
                throw new ArgumentException("The given names array must contains at least one element.");

            this.Names = names;
            this.MinValue = minValue;
        }

        public AppArgumentAttribute(string[] names, string minValue, string maxValue)
        {
            if (names == null)
                throw new ArgumentNullException("names");
            if (names.Length == 0)
                throw new ArgumentException("The given names array must contains at least one element.");

            this.Names = names;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}
