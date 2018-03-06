using System;
using System.Globalization;
using System.IO;
using System.Text;
using Consolification.Core.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consolification.Core.Test
{
	[TestClass]
	public class ArgumentsParserTest
	{
        private const string TextFileContent = "This is the content of a dummy file!\r\n\r\n" +
                                                "With 3 lines.";
        private const string TextFileContentUTF8 = "≠";
        private const string TextFileName = "dummy.txt";

        [TestMethod]
		public void ArgumentsParser_BooleanArgument()
		{
			string[] args = new string[1];
			args[0] = "/A";

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsFalse(data.MyBoolean1);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyBoolean1);
		}

		[TestMethod]
		public void ArgumentsParser_ByteArgument()
		{
			byte argValue = 128;
			string[] args = new string[2];
			args[0] = "/B";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyDouble1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyByte1 == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_SByteArgument()
		{
			sbyte argValue = 61;
			string[] args = new string[2];
			args[0] = "/SB";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MySByte == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MySByte == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_CharArgumentValid()
		{
			char argValue = 'h';
			string[] args = new string[] { "/C1", argValue.ToString() };

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyChar == default(char));

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyChar == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_CharArgumentNotValid()
		{
			char argValue = 'T'; // Only low letter alpha char are accepted (see associated attribute)
			string[] args = new string[] { "/C1", argValue.ToString() };

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyChar == default(char));

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("An exception must be thrown!");
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message == "The value of the argument /C1 cannot be lower than a");
			}
		}

		[TestMethod]
		public void ArgumentsParser_Int16Argument()
		{
			short argValue = 12350;
			string[] args = new string[2];
			args[0] = "/I16";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyShort1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyShort1 == argValue);


			argValue = 658;
			args = new string[2];
			args[0] = "/INTEGER16";
			args[1] = argValue.ToString();

			data = new SimpleDataMock();
			Assert.IsTrue(data.MyShort1 == 0);

			parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyShort1 == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_Int32Argument()
		{
			int argValue = 6532067;
			string[] args = new string[2];
			args[0] = "/I32";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyInteger1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyInteger1 == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_Int64Argument()
		{
			long argValue = 856498763251;
			string[] args = new string[2];
			args[0] = "/I64";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyLong1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyLong1 == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_UInt16Argument()
		{
			ushort argValue = 45963;
			string[] args = new string[2];
			args[0] = "/UI16";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyUShort == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyUShort == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_UInt32Argument()
		{
			uint argValue = 4033263035;
			string[] args = new string[2];
			args[0] = "/UI32";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyUInt == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyUInt == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_UInt64Argument()
		{
			ulong argValue = 40332630353540;
			string[] args = new string[2];
			args[0] = "/UI64";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyULong == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyULong == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_DecimalArgument()
		{
			decimal argValue = 856498763251.65m;
			string[] args = new string[] { "/DEC", argValue.ToString() };

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyDecimal == 0m);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyDecimal == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_SingleArgument()
		{
			float argValue = 234.4f;
			string[] args = new string[2];
			args[0] = "/SINGLE";
			args[1] = argValue.ToString(CultureInfo.InvariantCulture);

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MySingle == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MySingle == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_DoubleArgument()
		{
			double argValue = 65877065.654904;
			string[] args = new string[2];
			args[0] = "/D";
			args[1] = argValue.ToString(CultureInfo.InvariantCulture);

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyDouble1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyDouble1 == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_DateTimeArgument1()
		{
			string[] args = new string[] { "/STARTDATE", "2018/02/24" };

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.StartDate == DateTime.MinValue);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.StartDate == new DateTime(2018, 02, 24));
		}

		[TestMethod]
		public void ArgumentsParser_DateTimeArgument2()
		{
			string[] args = new string[] { "/STARTDATE", "2018/02/24 15:36:20" };

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.StartDate == DateTime.MinValue);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.StartDate == new DateTime(2018, 02, 24, 15, 36, 20));
		}

		[TestMethod]
		public void ArgumentsParser_UriArgument()
		{
			string[] args = new string[] { "/URI", "http://www.google.com" };

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyUri == default(Uri));

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyUri.AbsoluteUri == "http://www.google.com/");
		}

		[TestMethod]
		public void ArgumentsParser_VersionArgument()
		{
			string[] args = new string[] { "/VERSION", "1.5.2" };

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyVersion == default(Version));

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyVersion.ToString() == "1.5.2");
		}

        [TestMethod]
        public void ArgumentsParser_CharArrayArgument()
        {
            string[] args = new string[] { "/CHARARRAY", "This is a string!" };

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsNull(data.CharArray);

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);
            Assert.IsNotNull(data.CharArray);
            Assert.IsTrue(data.CharArray.Length == args[1].Length);
            Assert.IsTrue(data.CharArray[1] == 'h');
        }

        [TestMethod]
        public void ArgumentsParser_ByteArrayArgument()
        {
            string[] args = new string[] { "/BYTEARRAY", "This is a string!" };

            SimpleDataMock data = new SimpleDataMock();
            Assert.IsNull(data.ByteArray);

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);
            Assert.IsNotNull(data.ByteArray);
            Assert.IsTrue(data.ByteArray.Length == args[1].Length);
            Assert.IsTrue(data.ByteArray[1] == 'h');
        }

        [TestMethod]
		public void ArgumentsParser_StringArgument()
		{
			string[] args = new string[2];
			args[0] = "/S";
			args[1] = "This is an argument";

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyDouble1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyString1 == args[1]);
		}

		[TestMethod]
		public void ArgumentsParser_MinMaxArgument_OK()
		{
			byte argValue = 12;
			string[] args = new string[2];
			args[0] = "/B2";
			args[1] = argValue.ToString();

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyByte2 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyByte2 == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_MinMaxArgument_MaxExceed()
		{
			string[] args = new string[2];
			args[0] = "/B2";
			args[1] = "50";

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyByte2 == 0);

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("An exception must be thrown!");
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("cannot be greater than"));
			}
		}

		[TestMethod]
		public void ArgumentsParser_MinMaxArgument_MinExceed()
		{
			string[] args = new string[2];
			args[0] = "/B2";
			args[1] = "3";

			SimpleDataMock data = new SimpleDataMock();
			Assert.IsTrue(data.MyByte2 == 0);

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("An exception must be thrown!");
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("cannot be lower than"));
			}
		}

		[TestMethod]
		public void ArgumentsParser_MandatoryArgument_Missing()
		{
			string[] args = new string[0];
			UserCredetentialDataMock data = new UserCredetentialDataMock();

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("An exception must be thrown!");
			}
			catch (MissingMandatoryArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("is missing"));
			}
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_NoArg()
		{
			string[] args = new string[0];
			SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

			// We must not have any exception as the mandatory argument is 
			// a child of a parent argument that is itself not mandatory and not present.
			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_TopParentArgOnly()
		{
			string[] args = new string[] { "/TOP", "1" };

			SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

			// We must not have any exception as the mandatory argument is 
			// a child of a parent argument that is itself not mandatory and not present.
			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_TopAndMidParentsArgOnly()
		{
			string[] args = new string[] { "/TOP", "1", "/MID", "2" };

			SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("A MissingArgumentException exception must be thrown as /MID is present and there is a child mandatory argument.");
			}
			catch (MissingMandatoryArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("The mandatory argument /CHILD2 is missing."));
			}
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_MidParentArgOnly()
		{
			string[] args = new string[] { "/MID", "2" };

			SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("A MissingArgumentException exception must be thrown as /MID is present but its parent is not present.");
			}
			catch (MissingParentArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("The parent argument /TOP is missing."));
			}
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_Child2ArgOnly()
		{
			string[] args = new string[] { "/CHILD2", "22" };

			SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("A MissingArgumentException exception must be thrown as /MID & /TOP are not present.");
			}
			catch (MissingParentArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("The parent argument /MID is missing."));
			}
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_TopAndChild1ParentArgOnly()
		{
			string[] args = new string[] { "/TOP", "1", "/CHILD1", "C1" };

			SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

			// We must not have any exception as the mandatory argument is 
			// a child of a parent argument that is itself not mandatory and not present.
			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_AllArgs()
		{
			string[] args = new string[] { "/TOP", "1", "/CHILD1", "C1", "/MID", "32", "/CHILD1", "C1", "/CHILD2", "C2", "/CHILD3", "C3" };

			SimpleHierarchyDataMock data = new SimpleHierarchyDataMock();

			// Everything must be fine!
			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);

			Assert.IsTrue(data.TopArg == "1");
			Assert.IsTrue(data.MidArg == "32");
			Assert.IsTrue(data.ChildArg1 == "C1");
			Assert.IsTrue(data.ChildArg2 == "C2");
			Assert.IsTrue(data.ChildArg3 == "C3");
		}

		[TestMethod]
		public void ArgumentsParser_DuplicateArgument()
		{
			string[] args = new string[] { "/DUP", "1" };
			DuplicateDataMock data = new DuplicateDataMock();

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("A InvalidArgumentDefinitionException as '/DUP' argument is specified 2 times in DuplicateDataMock.");
			}
			catch (InvalidArgumentDefinitionException e)
			{
				Assert.IsTrue(e.Message.Contains("One of the argument specified with the property Duplicated2 has been already registered."));
			}
		}

		[TestMethod]
		public void ArgumentsParser_ComplexHierarchy()
		{
			string[] args = new string[] { "/ARG1", "/TOPA", "topa" };
			ComplexHierarchyDataMock data = new ComplexHierarchyDataMock();

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			ArgumentInfo[] argsInfo = parser.ArgumentsInfo.Hierarchy;

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

			Assert.IsTrue(argsInfo.Length == 5);
			Assert.IsTrue(argsInfo[0].Argument.Name == "/?");
			Assert.IsTrue(argsInfo[1].Argument.Name == "/ARG1");
			Assert.IsTrue(argsInfo[2].Argument.Name == "/TOPA");
			Assert.IsTrue(argsInfo[2].Children.Count == 2);
			Assert.IsTrue(argsInfo[2].Children[0].Argument.Name == "/CHILDTOPA1");
			Assert.IsTrue(argsInfo[2].Children[1].Argument.Name == "/MID");
			Assert.IsTrue(argsInfo[2].Children[1].Children.Count == 3);
			Assert.IsTrue(argsInfo[2].Children[1].Children[0].Argument.Name == "/CHILDMID1");
			Assert.IsTrue(argsInfo[2].Children[1].Children[1].Argument.Name == "/CHILDMID2");
			Assert.IsTrue(argsInfo[2].Children[1].Children[2].Argument.Name == "/BACK");
			Assert.IsTrue(argsInfo[2].Children[1].Children[2].Children.Count == 2);
			Assert.IsTrue(argsInfo[2].Children[1].Children[2].Children[0].Argument.Name == "/CHILDBACK2");
			Assert.IsTrue(argsInfo[2].Children[1].Children[2].Children[1].Argument.Name == "/CHILDBACK1");

			Assert.IsTrue(argsInfo[3].Argument.Name == "/TOPB");
			Assert.IsTrue(argsInfo[3].Children.Count == 3);
			Assert.IsTrue(argsInfo[3].Children[0].Argument.Name == "/MIDB");
			Assert.IsTrue(argsInfo[3].Children[0].Children.Count == 2);
			Assert.IsTrue(argsInfo[3].Children[0].Children[0].Argument.Name == "/CHILDMIDB2");
			Assert.IsTrue(argsInfo[3].Children[0].Children[1].Argument.Name == "/CHILDMIDB1");

			Assert.IsTrue(argsInfo[3].Children[1].Argument.Name == "/CHILDTOPB1");
			Assert.IsTrue(argsInfo[3].Children[2].Argument.Name == "/CHILDTOPB2");

			Assert.IsTrue(argsInfo[4].Argument.Name == "/ARG2");
		}

		[TestMethod]
		public void ArgumentsParser_InvalidArgumentType()
		{
			string[] args = new string[] { "/H", "123" };
			InvalidDataMock data = new InvalidDataMock();
			ArgumentsParser parser = new ArgumentsParser();
			try
			{
				parser.Parse(data, args);
				Assert.Fail("A NotSupportedException exception must be thrown!");
			}
			catch (NotSupportedException e)
			{
				Assert.IsTrue(e.Message == "The type of the argument '/H' is not supported.");
			}
		}

		[TestMethod]
		public void ArgumentsParser_UnknownArgument()
		{
			string[] args = new string[] { "/YES" };
			SimpleDataMock data = new SimpleDataMock();
			ArgumentsParser parser = new ArgumentsParser();
			try
			{
				parser.Parse(data, args);
				Assert.Fail("A UnknownArgumentException exception must be thrown!");
			}
			catch (UnknownArgumentException e)
			{
				Assert.IsTrue(e.Message == "Unknown argument /YES.");
			}
		}

        [TestMethod]
        public void ArgumentsParser_FileContentAttribute_OK()
        {
            string filePath = GetDummyTextFilePath();
            try
            {
                File.WriteAllText(filePath, TextFileContent);

                string[] args = new string[] { "/FILE", filePath };
                FileDataMock data = new FileDataMock();

                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);

                Assert.IsNotNull(data.FileByteArray);
                Assert.IsTrue(data.FileByteArray.Length == TextFileContent.Length);
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void ArgumentsParser_FileContentAttribute_NoFile()
        {
            string filePath = GetDummyTextFilePath();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            string[] args = new string[] { "/FILE", filePath };
            FileDataMock data = new FileDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("An ArgumentException exception must be thrown as the specified file does not exist.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message == "Invalid specified value for the argument /FILE.");
                Assert.IsNotNull(e.InnerException);
                Assert.IsTrue(e.InnerException.Message == "The file path specified with the argument '/FILE' does not exist.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_FileContentAttribute_Lines()
        {
            string filePath = GetDummyTextFilePath();
            try
            {
                File.WriteAllText(filePath, TextFileContent);

                string[] args = new string[] { "/FILELINES", filePath };
                FileDataMock data = new FileDataMock();

                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);

                Assert.IsNotNull(data.FileLines);
                Assert.IsTrue(data.FileLines.Length == 3);
                Assert.IsTrue(data.FileLines[1] == "");
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void ArgumentsParser_FileContentAttribute_String()
        {
            string filePath = GetDummyTextFilePath();
            try
            {
                File.WriteAllText(filePath, TextFileContentUTF8, Encoding.UTF8);

                string[] args = new string[] { "/FILESTRING", filePath };
                FileDataMock data = new FileDataMock();

                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);

                Assert.IsNotNull(data.FileString);
                Assert.IsTrue(data.FileString.Length == 1);
                Assert.IsTrue(data.FileString == TextFileContentUTF8);
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void ArgumentsParser_FileContentAttribute_CharArray()
        {
            string filePath = GetDummyTextFilePath();
            try
            {
                File.WriteAllText(filePath, TextFileContent);

                string[] args = new string[] { "/FILECHAR", filePath };
                FileDataMock data = new FileDataMock();

                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);

                Assert.IsNotNull(data.FileCharArray);
                Assert.IsTrue(data.FileCharArray.Length == TextFileContent.Length);
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void ArgumentsParser_FileContentAttribute_FileStream()
        {
            string filePath = GetDummyTextFilePath();
            try
            {
                File.WriteAllText(filePath, TextFileContent);

                string[] args = new string[] { "/FILESTREAM", filePath };
                FileDataMock data = new FileDataMock();

                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);

                Assert.IsNotNull(data.FileStream);
                data.FileStream.Close();
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void ArgumentsParser_MandatoryArg_OK()
        {
            string[] args = new string[] { "/D1", "1", "/D2", "2", "/D3", "3", "/D4", "4" };
            MandatoryDataMock data = new MandatoryDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsTrue(data.Data1 == 1);
            Assert.IsTrue(data.Data2 == 2);
            Assert.IsTrue(data.Data3 == 3);
            Assert.IsTrue(data.Data4 == "4");
        }

        [TestMethod]
        public void ArgumentsParser_MandatoryArg_MissingD4()
        {
            string[] args = new string[] { "/D1", "1", "/D2", "2", "/D3", "3" };
            MandatoryDataMock data = new MandatoryDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            ConsoleWrapperMock console = new ConsoleWrapperMock();
            console.PasswordOutput = "444";
            parser.Console = console;
            parser.PasswordReader = new DefaultPasswordReader(parser.Console);
            parser.Parse(data, args);

            Assert.IsTrue(data.Data1 == 1);
            Assert.IsTrue(data.Data2 == 2);
            Assert.IsTrue(data.Data3 == 3);
            Assert.IsTrue(data.Data4 == "444"); // Retrieved via IPasswordReader
        }

        [TestMethod]
        public void ArgumentsParser_MandatoryArg_MissingD3()
        {
            string[] args = new string[] { "/D1", "1", "/D2", "2", "/D4", "4" };
            MandatoryDataMock data = new MandatoryDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            ConsoleWrapperMock console = new ConsoleWrapperMock();
            console.ReadLineOutput = "333";
            parser.Console = console;
            parser.Parse(data, args);

            Assert.IsTrue(data.Data1 == 1);
            Assert.IsTrue(data.Data2 == 2);
            Assert.IsTrue(data.Data3 == 333);
            Assert.IsTrue(data.Data4 == "4");
        }

        [TestMethod]
        public void ArgumentsParser_MandatoryArg_MissingD3AndD4()
        {
            string[] args = new string[] { "/D1", "1", "/D2", "2" };
            MandatoryDataMock data = new MandatoryDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            ConsoleWrapperMock console = new ConsoleWrapperMock();
            console.ReadLineOutput = "699";
            console.PasswordOutput = "abcght";
            parser.Console = console;
            parser.PasswordReader = new DefaultPasswordReader(parser.Console);
            parser.Parse(data, args);

            Assert.IsTrue(data.Data1 == 1);
            Assert.IsTrue(data.Data2 == 2);
            Assert.IsTrue(data.Data3 == 699);
            Assert.IsTrue(data.Data4 == "abcght");
        }

        [TestMethod]
        public void ArgumentsParser_MandatoryArg_MissingD1()
        {
            string[] args = new string[] { "/D2", "2", "/D3", "3", "/D4", "4" };
            MandatoryDataMock data = new MandatoryDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsTrue(data.Data1 == 0);
            Assert.IsTrue(data.Data2 == 2);
            Assert.IsTrue(data.Data3 == 3);
            Assert.IsTrue(data.Data4 == "4");
        }

        [TestMethod]
        public void ArgumentsParser_MandatoryArg_MissingD2()
        {
            string[] args = new string[] { "/D1", "1", "/D3", "3", "/D4", "4" };
            MandatoryDataMock data = new MandatoryDataMock();

            try
            {
                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
                Assert.Fail("A MissingMandatoryArgumentException exception must be thrown.");
            }
            catch (MissingMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message == "The mandatory argument /D2 is missing.");
            }
        }

        #region Private Methods
        private string GetDummyTextFilePath()
        {
            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Consolification");
            if (Directory.Exists(dirPath) == false)
            {
                Directory.CreateDirectory(dirPath);
            }
            return Path.Combine(dirPath, TextFileName);
        }
        #endregion
    }
}
