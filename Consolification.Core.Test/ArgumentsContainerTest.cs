using System;
using System.Globalization;
using Consolification.Core.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
    [TestClass]
    public class ArgumentsContainerTest
    {
        [TestMethod]
        public void ArgumentsContainer_BooleanArgument()
        {
            string[] args = new string[1];
            args[0] = "/A";

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsFalse(data.MyBoolean1);

            data.Initialize(args);
            Assert.IsTrue(data.MyBoolean1);
        }

        [TestMethod]
        public void ArgumentsContainer_ByteArgument()
        {
            byte argValue = 128;
            string[] args = new string[2];
            args[0] = "/B";
            args[1] = argValue.ToString();

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyDouble1 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyByte1 == argValue);
        }

        [TestMethod]
        public void ArgumentsContainer_Int16Argument()
        {
            short argValue = 12350;
            string[] args = new string[2];
            args[0] = "/I16";
            args[1] = argValue.ToString();

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyShort1 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyShort1 == argValue);


            argValue = 658;
            args = new string[2];
            args[0] = "/INTEGER16";
            args[1] = argValue.ToString();

            data = new SimpleDataMock();
            Assert.IsTrue(data.MyShort1 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyShort1 == argValue);
        }

        [TestMethod]
        public void ArgumentsContainer_Int32Argument()
        {
            int argValue = 6532067;
            string[] args = new string[2];
            args[0] = "/I32";
            args[1] = argValue.ToString();

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyInteger1 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyInteger1 == argValue);
        }

        [TestMethod]
        public void ArgumentsContainer_Int64Argument()
        {
            long argValue = 856498763251;
            string[] args = new string[2];
            args[0] = "/I64";
            args[1] = argValue.ToString();

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyLong1 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyLong1 == argValue);
        }

        [TestMethod]
        public void ArgumentsContainer_DoubleArgument()
        {
            double argValue = 65877065.654904;
            string[] args = new string[2];
            args[0] = "/D";
            args[1] = argValue.ToString(CultureInfo.InvariantCulture);

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyDouble1 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyDouble1 == argValue);
        }

        [TestMethod]
        public void ArgumentsContainer_StringArgument()
        {
            string[] args = new string[2];
            args[0] = "/S";
            args[1] = "This is an argument";

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyDouble1 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyString1 == args[1]);
        }

        [TestMethod]
        public void ArgumentsContainer_MinMaxArgument_OK()
        {
            byte argValue = 12;
            string[] args = new string[2];
            args[0] = "/B2";
            args[1] = argValue.ToString();

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyByte2 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyByte2 == argValue);
        }

        [TestMethod]
        public void ArgumentsContainer_MinMaxArgument_MaxExceed()
        {
            string[] args = new string[2];
            args[0] = "/B2";
            args[1] = "50";

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyByte2 == 0);

            try
            {
                data.Initialize(args);
                Assert.Fail("An exception must be thrown!");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("cannot be greater than"));
            }
        }

        [TestMethod]
        public void ArgumentsContainer_MinMaxArgument_MinExceed()
        {
            string[] args = new string[2];
            args[0] = "/B2";
            args[1] = "3";

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsTrue(data.MyByte2 == 0);

            try
            {
                data.Initialize(args);
                Assert.Fail("An exception must be thrown!");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("cannot be lower than"));
            }
        }

        [TestMethod]
        public void ArgumentsContainer_MandatoryArgument_Missing()
        {
            string[] args = new string[0];
            UserCredetentialDataMock data = new UserCredetentialDataMock();

            try
            {
                data.Initialize(args);
                Assert.Fail("An exception must be thrown!");
            }
            catch (MissingMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("is missing"));
            }
        }

        [TestMethod]
        public void ArgumentsContainer_Hierarchy_NoArg()
        {
            string[] args = new string[0];
            SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

            // We must not have any exception as the mandatory argument is 
            // a child of a parent argument that is itself not mandatory and not present.
            data.Initialize(args);
        }

        [TestMethod]
        public void ArgumentsContainer_Hierarchy_TopParentArgOnly()
        {
            string[] args = new string[] { "/TOP", "1" };

            SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();
            
            // We must not have any exception as the mandatory argument is 
            // a child of a parent argument that is itself not mandatory and not present.
            data.Initialize(args);
        }

        [TestMethod]
        public void ArgumentsContainer_Hierarchy_TopAndMidParentsArgOnly()
        {
            string[] args = new string[] { "/TOP", "1", "/MID", "2" };

            SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

            try
            {
                data.Initialize(args);
                Assert.Fail("A MissingArgumentException exception must be thrown as /MID is present and there is a child mandatory argument.");
            }
            catch (MissingMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("The mandatory argument /CHILD2 is missing."));
            }
        }

        [TestMethod]
        public void ArgumentsContainer_Hierarchy_MidParentArgOnly()
        {
            string[] args = new string[] { "/MID", "2" };

            SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

            try
            {
                data.Initialize(args);
                Assert.Fail("A MissingArgumentException exception must be thrown as /MID is present but its parent is not present.");
            }
            catch (MissingParentArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("The parent argument /TOP is missing."));
            }
        }

        [TestMethod]
        public void ArgumentsContainer_Hierarchy_Child2ArgOnly()
        {
            string[] args = new string[] { "/CHILD2", "22" };

            SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

            try
            {
                data.Initialize(args);
                Assert.Fail("A MissingArgumentException exception must be thrown as /MID & /TOP are not present.");
            }
            catch (MissingParentArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("The parent argument /MID is missing."));
            }
        }

        [TestMethod]
        public void ArgumentsContainer_Hierarchy_TopAndChild1ParentArgOnly()
        {
            string[] args = new string[] { "/TOP", "1", "/CHILD1", "C1" };

            SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

            // We must not have any exception as the mandatory argument is 
            // a child of a parent argument that is itself not mandatory and not present.
            data.Initialize(args);
        }

        [TestMethod]
        public void ArgumentsContainer_Hierarchy_AllArgs()
        {
            string[] args = new string[] { "/TOP", "1", "/CHILD1", "C1", "/MID", "32", "/CHILD1", "C1", "/CHILD2", "C2", "/CHILD3", "C3" };

            SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

            // Everything must be fine!
            data.Initialize(args);

            Assert.IsTrue(data.TopArg == "1");
            Assert.IsTrue(data.MidArg == "32");
            Assert.IsTrue(data.ChildArg1 == "C1");
            Assert.IsTrue(data.ChildArg2 == "C2");
            Assert.IsTrue(data.ChildArg3 == "C3");
        }

        [TestMethod]
        public void ArgumentsContainer_DuplicateArgument()
        {
            string[] args = new string[] { "/DUP", "1" };
            DuplicateDataMock data = new DuplicateDataMock();

            try
            {
                data.Initialize(args);
                Assert.Fail("A InvalidArgumentDefinitionException as '/DUP' argument is specified 2 times in DuplicateDataMock.");
            }
            catch (InvalidArgumentDefinitionException e)
            {
                Assert.IsTrue(e.Message.Contains("One of the argument specified with the property Duplicated2 has been already registered."));
            }
        }

        [TestMethod]
        public void ArgumentsContainer_ComplexHierarchy()
        {
            string[] args = new string[] { "/ARG1", "/TOPA", "topa" };
            ComplexHierarchyDataMock data = new ComplexHierarchyDataMock();

            data.Initialize(args);
            ArgumentInfo[] argsInfo = data.ArgumentsInfo.Hierarchy;

            /// ARG1
            /// TOPA (1)
            ///     CHILDTOPA1
            ///     MID (2)
            ///         CHILDMID1
            ///         CHILDMID2
            ///         BACK (3)
            ///             CHILDBACK2
            ///             CHILDBACK1
            /// TOPB (4)
            ///     MIDB (5)
            ///         CHILDMIDB2
            ///         CHILDMIDB1
            ///     CHILDTOPB1
            ///     CHILDTOPB2
            /// ARG2
            /// 

            Assert.IsTrue(argsInfo.Length == 4);
            Assert.IsTrue(argsInfo[0].Argument.Name == "/ARG1");
            Assert.IsTrue(argsInfo[1].Argument.Name == "/TOPA");
            Assert.IsTrue(argsInfo[1].Children.Count == 2);
            Assert.IsTrue(argsInfo[1].Children[0].Argument.Name == "/CHILDTOPA1");
            Assert.IsTrue(argsInfo[1].Children[1].Argument.Name == "/MID");
            Assert.IsTrue(argsInfo[1].Children[1].Children.Count == 3);
            Assert.IsTrue(argsInfo[1].Children[1].Children[0].Argument.Name == "/CHILDMID1");
            Assert.IsTrue(argsInfo[1].Children[1].Children[1].Argument.Name == "/CHILDMID2");
            Assert.IsTrue(argsInfo[1].Children[1].Children[2].Argument.Name == "/BACK");
            Assert.IsTrue(argsInfo[1].Children[1].Children[2].Children.Count == 2);
            Assert.IsTrue(argsInfo[1].Children[1].Children[2].Children[0].Argument.Name == "/CHILDBACK2");
            Assert.IsTrue(argsInfo[1].Children[1].Children[2].Children[1].Argument.Name == "/CHILDBACK1");

            Assert.IsTrue(argsInfo[2].Argument.Name == "/TOPB");
            Assert.IsTrue(argsInfo[2].Children.Count == 3);
            Assert.IsTrue(argsInfo[2].Children[0].Argument.Name == "/MIDB");
            Assert.IsTrue(argsInfo[2].Children[0].Children.Count == 2);
            Assert.IsTrue(argsInfo[2].Children[0].Children[0].Argument.Name == "/CHILDMIDB2");
            Assert.IsTrue(argsInfo[2].Children[0].Children[1].Argument.Name == "/CHILDMIDB1");

            Assert.IsTrue(argsInfo[2].Children[1].Argument.Name == "/CHILDTOPB1");
            Assert.IsTrue(argsInfo[2].Children[2].Argument.Name == "/CHILDTOPB2");

            Assert.IsTrue(argsInfo[3].Argument.Name == "/ARG2");
        }
    }
}
