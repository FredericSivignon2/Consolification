using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public interface IJob<T> where T: new()
    {
        int Run(JobContext<T> context);
    }
}
