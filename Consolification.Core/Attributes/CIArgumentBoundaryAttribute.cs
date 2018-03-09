using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Controls argument values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CIArgumentBoundaryAttribute : Attribute
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the minimum allowed value for the associated argument.
        /// </summary>
        public string MinValue { get; private set; } = null;
        /// <summary>
        /// Gets or sets the maximum allowed value for the associated argument.
        /// </summary>
        public string MaxValue { get; private set; } = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes de new instance of the <cref>CIArgumentBoundaryAttribute</cref> class the the 
        /// given minimum value.
        /// </summary>
        /// <param name="minValue">The minimum value allowed for the associated argument.</param>
        public CIArgumentBoundaryAttribute(string minValue)
        {
            this.MinValue = minValue;
        }

        /// <summary>
        /// Initializes de new instance of the <cref>CIArgumentBoundaryAttribute</cref> class the the 
        /// given minimum and maximum value.
        /// </summary>
        /// <param name="minValue">The minimum value allowed for the associated argument.</param>
        /// <param name="maxValue">The maximum value allowed for the associated argument.</param>
        public CIArgumentBoundaryAttribute(string minValue, string maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
        #endregion
    }
}
