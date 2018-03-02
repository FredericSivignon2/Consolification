using System;
using System.Security;
using Consolification.Core.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
	[TestClass]
	public class DefaultConsoleReaderTest
	{
		[TestMethod]
		public void DefaultConsoleReader_ValidPassword()
		{
			string password = "dummy!pass23";
			ConsoleWrapperMock console = new ConsoleWrapperMock(password);
			DefaultConsoleReader reader = new DefaultConsoleReader(console);

			SecureString sstr = reader.GetPassword('*');

			Assert.IsTrue(console.Output == "************");
			Assert.IsTrue(sstr.Length == password.Length);
		}
	}
}
