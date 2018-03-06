using System;
using System.Security;
using Consolification.Core.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
	[TestClass]
	public class DefaultPasswordReaderTest
	{
		[TestMethod]
		public void DefaultConsoleReader_ValidPassword()
		{
			string password = "dummy!pass23";
			ConsoleWrapperMock console = new ConsoleWrapperMock();
            console.PasswordOutput = password;
			DefaultPasswordReader reader = new DefaultPasswordReader(console);

			SecureString sstr = reader.GetSecurePassword('*');

			Assert.IsTrue(console.Output == "************");
			Assert.IsTrue(sstr.Length == password.Length);
		}
	}
}
