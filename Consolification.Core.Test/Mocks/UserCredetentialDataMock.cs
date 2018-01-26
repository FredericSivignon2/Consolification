using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class UserCredetentialDataMock : ArgumentsContainer
    {
        [CIArgument("/U")]
        [CIMandatoryArgument]
        public string UserName { get; set; }

        [CIArgument("/S")]
        public string UserPassword { get; set; }
    }
}
