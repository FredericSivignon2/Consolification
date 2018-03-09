using Consolification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.QuickExample
{
    class MessageJob : IJob<Data>
    {
        public int Run(JobContext<Data> context)
        {
            context.Console.WriteLine(context.Data.Message);
            return 0;
        }
    }
}
