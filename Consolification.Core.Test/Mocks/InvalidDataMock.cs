using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consolification.Core.Attributes;

namespace Consolification.Core.Test.Mocks
{
	public class InvalidDataMock
	{
		[CIArgument("/H")]
		public EventHandler Handler { get; private set; }
	}
}
