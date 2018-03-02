using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class JobContext<T> where T : new()
    {
        public ILogWriter Logger { get; internal set; }
        public IConsoleReader Reader { get; internal set; }
        public T Data { get; internal set; }
    }
}
