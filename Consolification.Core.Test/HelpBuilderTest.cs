using System;
using Consolification.Core.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
    [TestClass]
    public class HelpBuilderTest
    {
        [TestMethod]
        public void HelpBuilder_GetHelpLines()
        {
            string[] args = new string[] { "-data2", "1" };

            HelpDataMock data = new HelpDataMock();
            data.Initialize(args);

            HelpBuilder builder = new HelpBuilder(data);
            string[] lines = builder.GetHelpLines();

            Assert.IsNotNull(lines);
            Assert.IsTrue(lines.Length == 6);
            Assert.IsTrue(lines[0] == "This is a dummy class for test purpose.");
            Assert.IsTrue(lines[1] == "");
            Assert.IsTrue(lines[2] == "Usage: Consolification.Core [--help] [-data1] -data2");
            Assert.IsTrue(lines[3] == "");
            Assert.IsTrue(lines[4] == "-data1 This is the data1 parameter.");
            Assert.IsTrue(lines[5] == "-data2 This is the data2 parameter.");
        }

        [TestMethod]
        public void HelpBuilder_GetHelpLines_OnComplexHierarchy()
        {
            string[] args = new string[] { "/?" };
            ComplexHierarchyDataMock data = new ComplexHierarchyDataMock();
            data.Initialize(args);

            HelpBuilder builder = new HelpBuilder(data);
            string[] lines = builder.GetHelpLines();

            Assert.IsNotNull(lines);
        }
    }
}
