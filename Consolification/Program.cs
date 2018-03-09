﻿using Consolification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification
{
    class Program
    {
        static int Main(string[] args)
        {
            ConsolificationEngine<RequestData> engine = new ConsolificationEngine<RequestData>();
            return engine.Start(args);
        }
    }
}
