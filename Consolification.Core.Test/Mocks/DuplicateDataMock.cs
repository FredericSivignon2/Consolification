﻿using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class DuplicateDataMock
    {
        [CINamedArgument("/DUP")]
        public string Duplicated1 { get; private set; }

        [CINamedArgument("/DUP")]
        public string Duplicated2 { get; private set; }
    }
}
