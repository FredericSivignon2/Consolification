using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Attributes
{
    public class CIJobAttribute : Attribute
    {
        public Type JobType { get; private set; }

        public CIJobAttribute(Type jobType)
        {
            this.JobType = jobType;
        }
    }
}
