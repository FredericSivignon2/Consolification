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
            string[] args = new string[] { @"C:\source\file.txt", "-method", "1" };

            HelpDataMock data = new HelpDataMock();
            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            HelpBuilder builder = new HelpBuilder(parser);
            string[] lines = builder.GetHelpLines();

            Assert.IsNotNull(lines);
            Assert.IsTrue(lines.Length == 8);
            Assert.IsTrue(lines[0] == "This is a dummy class for test purpose.");
            Assert.IsTrue(lines[1] == "");
            Assert.IsTrue(lines[2] == "Usage: Consolification.Core [--help] source [destination] [-format <formatValue>] -method <methodValue>");
            Assert.IsTrue(lines[3] == "");
            Assert.IsTrue(lines[4] == "source        The source file path to copy.");
            Assert.IsTrue(lines[5] == "destination   The destination path.");
            Assert.IsTrue(lines[6] == "-format       This is the format parameter.");
            Assert.IsTrue(lines[7] == "-method       This is the method parameter.");
        }

        //[TestMethod]
        public void HelpBuilder_GetHelpLines_OnComplexHierarchy()
        {
            string[] args = new string[] { "/?" };
            //ComplexHierarchyDataMock data = new ComplexHierarchyDataMock();
            ArgumentsParser parser = new ArgumentsParser();
            //parser.Parse(data, args);

            HelpBuilder builder = new HelpBuilder(parser);
            string[] lines = builder.GetHelpLines();

            Assert.IsNotNull(lines);
        }

        [TestMethod]
        public void HelpBuilder_GetHelpLines_OnComplexParentData()
        {
            string[] args = new string[] { "/?" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            HelpBuilder builder = new HelpBuilder(parser);
            string[] lines = builder.GetHelpLines();

            Assert.IsNotNull(lines);
            Assert.IsTrue(lines[0] == "Usage: Consolification.Core [/?] [/CHILD2 child2data [/CHILD1 [/CHILDVALUE1 <value>]]] [/SECONDARG <value>]");
            Assert.IsTrue(lines[1] == "");
            Assert.IsTrue(lines[2] == "/?           This is a complex example of inner class support.");
            Assert.IsTrue(lines[3] == "/CHILD2      Child 2");
            Assert.IsTrue(lines[4] == "    child2data   Data for child 2.");
            Assert.IsTrue(lines[5] == "    /CHILD1      Child 1 from child 2.");
            Assert.IsTrue(lines[6] == "        /CHILDVALUE1   This is the child value 1.");
            Assert.IsTrue(lines[7] == "/SECONDARG   This is a second argument.");
        }
    }
}
