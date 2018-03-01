using System;
using Consolification.Core.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
    [TestClass]
    public class ConsolificationEngineTest
    {
        [TestMethod]
        public void ConsolificationEngine_SimpleExe()
        {
            string[] args = new string[2];
            args[0] = "/A";
            args[1] = "123456ABCDEF";

            ConsolificationEngine<DataJobMock> engine = new ConsolificationEngine<DataJobMock>();
            Assert.IsTrue(string.IsNullOrEmpty(engine.Data.In));
            Assert.IsTrue(string.IsNullOrEmpty(engine.Data.Out));

            engine.Start(args);

            Assert.IsTrue(engine.Data.Out == engine.Data.In);
        }
    }
}
