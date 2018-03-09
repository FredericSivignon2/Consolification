using Consolification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.QuickExample
{
    class Program
    {
        static int Main(string[] args)
        {
            ConsolificationEngine<Data> engine = new ConsolificationEngine<Data>();
            return engine.Start(args);
        }
    }
}
