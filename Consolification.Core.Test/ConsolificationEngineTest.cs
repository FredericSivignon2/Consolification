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


            DataJobMock data = new DataJobMock();
            Assert.IsTrue(string.IsNullOrEmpty(data.In));
            Assert.IsTrue(string.IsNullOrEmpty(data.Out));
            
            data.Initialize(args);
            Assert.IsTrue(data.In == args[1]);
            Assert.IsTrue(string.IsNullOrEmpty(data.Out));

            ConsolificationEngine engine = new ConsolificationEngine(data);
            engine.Start();

            Assert.IsTrue(data.Out == data.In);
        }
    }
}
