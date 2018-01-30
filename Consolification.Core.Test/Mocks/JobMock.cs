﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class JobMock : IJob
    {
        public void Run(ArgumentsContainer container)
        {
            DataJobMock data = container as DataJobMock;
            if (data == null)
                throw new InvalidOperationException("data is null or is not a DataJobMock!");

            data.Out = data.In;
        }
    }
}
