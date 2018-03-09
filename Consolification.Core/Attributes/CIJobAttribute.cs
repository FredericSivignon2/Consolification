using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    /// <summary>
    /// Specifies which Job class will be instantiated and executed when
    /// the <cref>Consolification.Core.ConsolificationEngine</cref>.Start() method is called.
    /// You should put all your command logic within a Job.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CIJobAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <cref>Consolification.Core.Attributes.CIJobAttribute</cref> class
        /// with the given class Type.
        /// </summary>
        /// <param name="jobType"></param>
        public CIJobAttribute(Type jobType)
        {
            if (jobType == null)
                throw new ArgumentNullException("jobType");
            try
            {
                Type jobInterface = jobType.GetInterfaces().First<Type>((interType) =>
                {
                    return interType.FullName.Contains("Consolification.Core.IJob");
                });
            }
            catch (System.InvalidOperationException)
            {
                throw new InvalidArgumentTypeException(string.Format("The type '{0}' does not implement the Consolification.Core.IJob interface.", jobType.FullName));
            }

            this.JobType = jobType;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the Type of the class that implements the <cref>Consolification.Core.IJob</cref> interface.
        /// </summary>
        public Type JobType { get; private set; }
        #endregion
    }
}
