using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    // Should also be available for Class!!! to avoid to have a specific arg to trigger a job
    // We must be able to trigger a job even if there is no arg.
    public class CIJobAttribute : Attribute
    {
        public Type JobType { get; private set; }

        public CIJobAttribute(Type jobType)
        {
            this.JobType = jobType;
        }
    }
}
