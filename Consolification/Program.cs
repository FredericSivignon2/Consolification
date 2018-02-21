using Consolification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestData data = new RequestData();
            data.Initialize(args);

            ConsolificationEngine engine = new ConsolificationEngine(data);
            DefaultLogWriter writer = engine.Logger as DefaultLogWriter;
            writer.ShowLevelPrefix = false;

            engine.Start();
        }
    }
}
