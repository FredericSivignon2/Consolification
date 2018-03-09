using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class JobMock : IJob<JobDataMock>
    {
        public int Run(JobContext<JobDataMock> context)
        {
            JobDataMock data = context.Data;
            if (data == null)
                throw new InvalidOperationException("data is null or is not a DataJobMock!");

            data.Out = data.In;
            return 0;
        }
    }
}
