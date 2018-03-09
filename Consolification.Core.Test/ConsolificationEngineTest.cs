using System;
using Consolification.Core.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
    [TestClass]
    public class ConsolificationEngineTest
    {
        [TestMethod]
        public void ConsolificationEngine_PropertyJobSimpleExe()
        {
            string[] args = new string[2];
            args[0] = "/A";
            args[1] = "123456ABCDEF";

            ConsolificationEngine<JobDataMock> engine = new ConsolificationEngine<JobDataMock>();
            Assert.IsTrue(string.IsNullOrEmpty(engine.Data.In));
            Assert.IsTrue(string.IsNullOrEmpty(engine.Data.Out));

            int result = engine.Start(args);
            Assert.IsTrue(result == 0);
            Assert.IsTrue(engine.Data.Out == engine.Data.In);
        }

        [TestMethod]
        public void ConsolificationEngine_PropertyJobNotAJob()
        {
            string[] args = new string[2];
            args[0] = "/A";
            args[1] = "123456ABCDEF";

            ConsolificationEngine<BadJobDataMock> engine = new ConsolificationEngine<BadJobDataMock>();
            ConsoleWrapperMock console = new ConsoleWrapperMock();
            engine.Console = console;

            int result = engine.Start(args);
            Assert.IsTrue(result == engine.ResultCannotParseArguments);
            Assert.IsTrue(console.Output.Contains("does not implement the Consolification.Core.IJob interface."));
        }
    }
}
