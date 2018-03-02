using System;
using Consolification.Core.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
    [TestClass]
    public class ArgumentInfoTest
    {
        [TestMethod]
        public void ArgumentInfo_NullArg()
        {
            try
            {
                ArgumentInfo info = new ArgumentInfo(null);
                Assert.Fail("An ArgumentNullException must be thrown!");
            }
            catch (ArgumentNullException)
            {

            }

        }

        [TestMethod]
        public void ArgumentInfo_ToString()
        {
			CIArgumentAttribute attr = new CIArgumentAttribute("/A");
			ArgumentInfo info = new ArgumentInfo(attr);

			string result = info.ToString();
			Assert.IsTrue(result == "Argument: /A - Found: False");
        }
    }
}
