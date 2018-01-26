using System;
using System.Globalization;
using Consolification.Core.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
    [TestClass]
    public class ConsolificationEngineTest
    {
        [TestMethod]
        public void ConsolificationEngine_BooleanArgument()
        {
            string[] args = new string[1];
            args[0] = "/A";

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsFalse(data.MyBoolean1);

            data.Initialize(args);
            Assert.IsTrue(data.MyBoolean1);
        }

        [TestMethod]
        public void ConsolificationEngine_ByteArgument()
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
        public void ConsolificationEngine_Int16Argument()
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
            args[0] = "/SHORT";
            args[1] = argValue.ToString();

            data = new SimpleDataMock();
            Assert.IsTrue(data.MyShort1 == 0);

            data.Initialize(args);
            Assert.IsTrue(data.MyShort1 == argValue);
        }

        [TestMethod]
        public void ConsolificationEngine_Int32Argument()
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
        public void ConsolificationEngine_Int64Argument()
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
        public void ConsolificationEngine_DoubleArgument()
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
        public void ConsolificationEngine_StringArgument()
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
        public void ConsolificationEngine_MinMaxArgument_OK()
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
        public void ConsolificationEngine_MinMaxArgument_MaxExceed()
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
        public void ConsolificationEngine_MinMaxArgument_MinExceed()
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
        public void ConsolificationEngine_MandatoryArgument_Missing()
        {
            string[] args = new string[0];
            UserCredetentialDataMock data = new UserCredetentialDataMock();

            try
            {
                data.Initialize(args);
                Assert.Fail("An exception must be thrown!");
            }
            catch (MissingArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("is missing"));
            }
        }
    }
}
