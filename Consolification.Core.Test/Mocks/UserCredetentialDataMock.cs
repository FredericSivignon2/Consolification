using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    public class UserCredetentialDataMock : IArgumentsContainer
    {
        [AppArgument("/U")]
        [AppMandatoryArgument]
        public string UserName { get; set; }

        [AppArgument("/S")]
        public string UserPassword { get; set; }
    }
}
