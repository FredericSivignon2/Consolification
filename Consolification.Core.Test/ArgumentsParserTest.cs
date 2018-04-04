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
        public void ArgumentsParser_SimpleArgument()
        {
            string[] args = new string[] { "mysource", "mydestination" };
            SimpleArgumentDataMock data = new SimpleArgumentDataMock();
            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsTrue(data.Source == args[0]);
            Assert.IsTrue(data.Destination == args[1]);
        }

        [TestMethod]
		public void ArgumentsParser_BooleanArgument()
		{
			string[] args = new string[1];
			args[0] = "/A";

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
			Assert.IsTrue(data.MyChar == default(char));

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("An exception must be thrown!");
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message == "The value of the argument '/C1' cannot be lower than 'a'");
			}
		}

		[TestMethod]
		public void ArgumentsParser_Int16Argument()
		{
			short argValue = 12350;
			string[] args = new string[2];
			args[0] = "/I16";
			args[1] = argValue.ToString();

			AllDataTypeMock data = new AllDataTypeMock();
			Assert.IsTrue(data.MyShort1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyShort1 == argValue);


			argValue = 658;
			args = new string[2];
			args[0] = "/INTEGER16";
			args[1] = argValue.ToString();

			data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
			Assert.IsTrue(data.MyDouble1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyDouble1 == argValue);
		}

		[TestMethod]
		public void ArgumentsParser_DateTimeArgument1()
		{
			string[] args = new string[] { "/STARTDATE", "2018/02/24" };

			AllDataTypeMock data = new AllDataTypeMock();
			Assert.IsTrue(data.StartDate == DateTime.MinValue);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.StartDate == new DateTime(2018, 02, 24));
		}

		[TestMethod]
		public void ArgumentsParser_DateTimeArgument2()
		{
			string[] args = new string[] { "/STARTDATE", "2018/02/24 15:36:20" };

			AllDataTypeMock data = new AllDataTypeMock();
			Assert.IsTrue(data.StartDate == DateTime.MinValue);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.StartDate == new DateTime(2018, 02, 24, 15, 36, 20));
		}

		[TestMethod]
		public void ArgumentsParser_UriArgument()
		{
			string[] args = new string[] { "/URI", "http://www.google.com" };

			AllDataTypeMock data = new AllDataTypeMock();
			Assert.IsTrue(data.MyUri == default(Uri));

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyUri.AbsoluteUri == "http://www.google.com/");
		}

		[TestMethod]
		public void ArgumentsParser_VersionArgument()
		{
			string[] args = new string[] { "/VERSION", "1.5.2" };

			AllDataTypeMock data = new AllDataTypeMock();
			Assert.IsTrue(data.MyVersion == default(Version));

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyVersion.ToString() == "1.5.2");
		}

        [TestMethod]
        public void ArgumentsParser_CharArrayArgument()
        {
            string[] args = new string[] { "/CHARARRAY", "This is a string!" };

            AllDataTypeMock data = new AllDataTypeMock();
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

            AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
			Assert.IsTrue(data.MyDouble1 == 0);

			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
			Assert.IsTrue(data.MyString1 == args[1]);
		}

        [TestMethod]
        public void ArgumentsParser_StringArgument_TooLong()
        {
            string[] args = new string[2];
            args[0] = "/S";
            args[1] = "This is an argument with a too long text!";

            AllDataTypeMock data = new AllDataTypeMock();
            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("An ArgumentException expcetion must be thrown!");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message == "The length of the argument '/S' must be lower or equal to '20'.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_StringArgument_TooShort()
        {
            string[] args = new string[2];
            args[0] = "/S";
            args[1] = "Too short!";

            AllDataTypeMock data = new AllDataTypeMock();
            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("An ArgumentException expcetion must be thrown!");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message == "The length of the argument '/S' must be equal or greater than '12'.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_StringArgument_EmailOk()
        {
            string[] args = new string[] { "/EMAIL", "frederic.sivignon@gmail.com" };

            AllDataTypeMock data = new AllDataTypeMock();
            ArgumentsParser parser = new ArgumentsParser();
            
            parser.Parse(data, args);
            Assert.IsTrue(data.EmailAddress == args[1]);
        }

        [TestMethod]
        public void ArgumentsParser_StringArgument_EmailBad()
        {
            string[] args = new string[] { "/EMAIL", "ThisIsBad" };

            AllDataTypeMock data = new AllDataTypeMock();
            ArgumentsParser parser = new ArgumentsParser();

            try
            {
                parser.Parse(data, args);
                Assert.Fail("An ArgumentException expcetion must be thrown!");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message == "The value of the argument '/EMAIL' has an invalid format.");
            }
        }

        [TestMethod]
		public void ArgumentsParser_MinMaxArgument_OK()
		{
			byte argValue = 12;
			string[] args = new string[2];
			args[0] = "/B2";
			args[1] = argValue.ToString();

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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

			AllDataTypeMock data = new AllDataTypeMock();
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
            ComplexParentDataMock data = new ComplexParentDataMock();

			// We must not have any exception as the mandatory argument is 
			// a child of a parent argument that is itself not mandatory and not present.
			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_TopParentArgOnly()
		{
			string[] args = new string[] { "/SECONDARG", "abc" };

            ComplexParentDataMock data = new ComplexParentDataMock();

			// We must not have any exception as the mandatory argument is 
			// a child of a parent argument that is itself not mandatory and not present.
			ArgumentsParser parser = new ArgumentsParser();
			parser.Parse(data, args);
		}

		[TestMethod]
		public void ArgumentsParser_Hierarchy_TopAndMidParentsArgOnly()
		{
			string[] args = new string[] { "/CHILD2" };

            ComplexParentDataMock data = new ComplexParentDataMock();

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
				parser.Parse(data, args);
				Assert.Fail("A MissingArgumentException exception must be thrown as /MID is present and there is a child mandatory argument.");
			}
			catch (MissingMandatoryArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("The mandatory argument 'child2data' is missing."));
			}
		}

        // Same test than above, but with other top level args
        [TestMethod]
        public void ArgumentsParser_Hierarchy_TopAndMidParentsArgOnly2()
        {
            string[] args = new string[] { "/CHILD2", "/SECONDARG", "abcdef" };

            ComplexParentDataMock data = new ComplexParentDataMock();

            try
            {
                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
                Assert.Fail("A MissingArgumentException exception must be thrown as /MID is present and there is a child mandatory argument.");
            }
            catch (MissingMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("The mandatory argument 'child2data' is missing."));
            }
        }

        [TestMethod]
        public void ArgumentsParser_Hierarchy_MidParentArgOnly()
		{
			string[] args = new string[] { "/STRARG1", "abv" };

            VeryComplexParentDataMock data = new VeryComplexParentDataMock();

			try
			{
				ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
				Assert.Fail("A MissingArgumentException exception must be thrown as /MID is present but its parent is not present.");
			}
			catch (MissingParentArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains("The parent argument '/VERYCOMPLEX' is missing for the argument '/STRARG1'."));
			}
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
			AllDataTypeMock data = new AllDataTypeMock();
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
        public void ArgumentsParser_UnknownArgumentInChild()
        {
            string[] args = new string[] { "/CHILD2", "abcdef", "/YES" };
            ComplexParentDataMock data = new ComplexParentDataMock();
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
        public void ArgumentsParser_FileDoesNotExist()
        {
            string filePath = "C:\abcdefgh.xyz";
            try
            {
                string[] args = new string[] { "/FILELINES", filePath };
                FileDataMock data = new FileDataMock();

                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
                Assert.Fail("A FileNotFoundException exception must be thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message == "Invalid specified value for the argument /FILELINES.");
                Assert.IsTrue(e.InnerException is FileNotFoundException);
                Assert.IsTrue(e.InnerException.Message == "The file path specified with the argument '/FILELINES' does not exist.");
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
                Assert.IsTrue(e.Message == "The mandatory argument '/D2' is missing.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_EmbeddedChild1DataSet()
        {
            string[] args = new string[] { "/PARENTVALUE", "1", "/CHILD1", "/CHILDVALUE1", "abcdef" };
            SimpleParentDataMock data = new SimpleParentDataMock();
            Assert.IsNull(data.Child1);
                       
            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsTrue(data.ParentValue == 1);
            Assert.IsNotNull(data.Child1);
            Assert.IsTrue(data.Child1.ChildValue1 == "abcdef");
        }

        [TestMethod]
        public void ArgumentsParser_EmbeddedChild1DataNotSet()
        {
            string[] args = new string[] { "/PARENTVALUE", "1" };
            SimpleParentDataMock data = new SimpleParentDataMock();
            Assert.IsNull(data.Child1);

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsTrue(data.ParentValue == 1);
            Assert.IsNull(data.Child1);
        }

        [TestMethod]
        public void ArgumentsParser_EmbeddedChild1DataMissingChild1()
        {
            string[] args = new string[] { "/INTARG1", "1" };
            VeryComplexParentDataMock data = new VeryComplexParentDataMock();
            Assert.IsNull(data.ChildData);

            try
            {
                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
                Assert.Fail("A MissingParentArgumentException must be thrown.");
            }
            catch (MissingParentArgumentException e)
            {
                Assert.IsTrue(e.Message == "The parent argument '/VERYCOMPLEX' is missing for the argument '/INTARG1'.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_EmbeddedChild2ParentMissing()
        {
            string[] args = new string[] { "/INTARG2", "1" };
            VeryComplexParentDataMock data = new VeryComplexParentDataMock();
            Assert.IsNull(data.ChildData);

            try
            {
                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
                Assert.Fail("A MissingParentArgumentException must be thrown.");
            }
            catch (MissingParentArgumentException e)
            {
                Assert.IsTrue(e.Message == "The parent argument '/CHILDVC2' is missing for the argument '/INTARG2'.");
            }
        }

        //
        // VeryComplexParentDataMock
        //      ComplexParentDataMock /COMPLEX
        //          Child2DataMock /CHILD2
        //              Child2Data child2data
        //              Child1DataMock /CHILD1
        //                  ChildValue1 /CHILDVALUE1
        //          SecondArg  /SECONDARG
        //      VeryComplexChildDataMock /VERYCOMPLEX
        //          IntArg1 /INTARG1
        //          StrArg1 /STRARG1
        //          VeryComplexChild2DataMock  /CHILDVC2
        //              IntArg2 /INTARG2
        //              StrArg2 /STRARG2
        //
        [TestMethod]
        public void ArgumentsParser_EmbeddedChild2MandatoryMissing()
        {
            string[] args = new string[] { "/COMPLEX", "/CHILD2", "ghjkl", "/VERYCOMPLEX", "/INTARG1", "256", "/CHILDVC2", "/STRARG2", "abcdef" };
            VeryComplexParentDataMock data = new VeryComplexParentDataMock();
            Assert.IsNull(data.ChildData);

            try
            {
                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
                Assert.Fail("A MissingParentArgumentException must be thrown.");
            }
            catch (MissingMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message == "The mandatory argument '/INTARG2' is missing.");
            }
        }
        
            [TestMethod]
        public void ArgumentsParser_DuplicateArgumentsComplex()
        {
            string[] args = new string[] { "/COMPLEX", "/INTARG1", "256" };
            BadVeryComplexDataMock data = new BadVeryComplexDataMock();
            
            try
            {
                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
                Assert.Fail("A MissingParentArgumentException must be thrown.");
            }
            catch (DuplicateArgumentDefinitionException e)
            {
                Assert.IsTrue(e.Message == "The definition of the argument '/INTARG2' already exists.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_VeryComplexParent_ValueMissing()
        {
            string[] args = new string[] { "/VERYCOMPLEX", "/INTARG1" };
            VeryComplexParentDataMock data = new VeryComplexParentDataMock();
            Assert.IsNull(data.ChildData);

            try
            {
                ArgumentsParser parser = new ArgumentsParser();
                parser.Parse(data, args);
                Assert.Fail("A MissingParentArgumentException must be thrown.");
            }
            catch (MissingArgumentValueException e)
            {
                Assert.IsTrue(e.Message == "The value of the argument '/INTARG1' is missing.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataAllSet()
        {
            string[] args = new string[] { "/CHILD2", "Hello!", "/CHILD1", "/CHILDVALUE1", "tuvwxyz", "/SECONDARG", "123456" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsNotNull(data.Child2Data);
            Assert.IsTrue(data.Child2Data.Child2Data == "Hello!");
            Assert.IsNotNull(data.Child2Data.Child1Data);
            Assert.IsTrue(data.Child2Data.Child1Data.ChildValue1 == "tuvwxyz");
            Assert.IsTrue(data.SecondArg == "123456");
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataAllSet_WrongOrder()
        {
            string[] args = new string[] { "/CHILD1", "/CHILDVALUE1", "tuvwxyz", "/CHILD2", "Hello!", "/SECONDARG", "123456" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A WrongArgumentPositionException exception must be thrown.");
            }
            catch (WrongArgumentPositionException e)
            {
                Assert.IsTrue(e.Message == "The argument '/CHILD2' must be placed before the argument '/CHILD1'.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataAllSet_WrongOrder2()
        {
            string[] args = new string[] { "/CHILD2", "/CHILD1", "Hello!", "/CHILDVALUE1", "tuvwxyz", "/SECONDARG", "123456" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A WrongArgumentPositionException exception must be thrown.");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Unknown argument Hello!.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataOnlyChild2()
        {
            string[] args = new string[] { "/CHILD2", "Hello!" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsNotNull(data.Child2Data);
            Assert.IsTrue(data.Child2Data.Child2Data == "Hello!");
            Assert.IsNull(data.Child2Data.Child1Data);
            Assert.IsNull(data.SecondArg);
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataOnlyChild2AndChild1()
        {
            string[] args = new string[] { "/CHILD2", "Hello!", "/CHILD1" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsNotNull(data.Child2Data);
            Assert.IsTrue(data.Child2Data.Child2Data == "Hello!");
            Assert.IsNotNull(data.Child2Data.Child1Data);
            Assert.IsNull(data.Child2Data.Child1Data.ChildValue1);
            Assert.IsNull(data.SecondArg);
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataSecondArgOnly()
        {
            string[] args = new string[] { "/SECONDARG", "789" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            
            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsNull(data.Child2Data);
            Assert.IsTrue(data.SecondArg == "789");
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataChild1Only()
        {
            string[] args = new string[] { "/CHILD1" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A MissingParentArgumentException exception must be thrown.");
            }
            catch (MissingParentArgumentException e)
            {
                Assert.IsTrue(e.Message == "The parent argument '/CHILD2' is missing for the argument '/CHILD1'.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataChildValue1Only()
        {
            string[] args = new string[] { "/CHILDVALUE1" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A MissingParentArgumentException exception must be thrown.");
            }
            catch (MissingParentArgumentException e)
            {
                Assert.IsTrue(e.Message == "The parent argument '/CHILD1' is missing for the argument '/CHILDVALUE1'.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataUnknowArg()
        {
            string[] args = new string[] { "--reset" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A UnknownArgumentException exception must be thrown.");
            }
            catch (UnknownArgumentException e)
            {
                Assert.IsTrue(e.Message == "Unknown argument --reset.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_ComplexParentDataMandatory()
        {
            string[] args = new string[] { "/CHILD2", "/CHILD1", "/CHILDVALUE1", "tuvwxyz", "/SECONDARG", "123456" };
            ComplexParentDataMock data = new ComplexParentDataMock();
            Assert.IsNull(data.Child2Data);

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A UnknownArgumentException exception must be thrown.");
            }
            catch (MissingMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message == "The mandatory argument 'child2data' is missing.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_MissingMandatorySimpleArgument()
        {
            string[] args = new string[] { "-format", "test" };
            HelpDataMock data = new HelpDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A UnknownArgumentException exception must be thrown.");
            }
            catch (MissingMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message == "The mandatory argument 'source' is missing.");
            }
        }

        [TestMethod]
        public void ArgumentsParser_HelpAloneWithMandatory()
        {
            string[] args = new string[] { "/?" };
            Child2DataMock data = new Child2DataMock();

            ArgumentsParser parser = new ArgumentsParser();
            
            parser.Parse(data, args);
            Assert.IsTrue(parser.MustDisplayHelp);
        }

        [TestMethod]
        public void ArgumentParser_SimpleAndNamed()
        {
            string[] args = new string[] { "--max", "65535" };
            SimpleAndNamesDataMock data = new SimpleAndNamesDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);
            Assert.IsNull(data.Source);
            Assert.IsTrue(data.Max == 65535);
        }

        [TestMethod]
        public void ArgumentParser_SimpleMandatoryAndNamed_MissingMandatory()
        {
            string[] args = new string[] { "--max", "65535" };
            SimpleMandatoryAndNamedDataMock data = new SimpleMandatoryAndNamedDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A MissingMandatoryArgumentException exception must be thrown.");
            }
            catch (MissingMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message == "The mandatory argument 'source' is missing.");
            }

        }

        [TestMethod]
        public void ArgumentParser_ExclusiveArgument_ExclusiveFirst()
        {
            string[] args = new string[] { "/A", "/C1", "a" };
            AllDataTypeMock data = new AllDataTypeMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A ExclusiveArgumentException exception must be thrown.");
            }
            catch (ExclusiveArgumentException e)
            {
                Assert.IsTrue(e.Message == "The argument '/A' cannot be used in conjonction with '/C1'.");
            }
        }

        [TestMethod]
        public void ArgumentParser_ExclusiveArgument_ExclusiveSecond()
        {
            string[] args = new string[] { "/C1", "a", "/A" };
            AllDataTypeMock data = new AllDataTypeMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A ExclusiveArgumentException exception must be thrown.");
            }
            catch (ExclusiveArgumentException e)
            {
                Assert.IsTrue(e.Message == "The argument '/A' cannot be used in conjonction with '/C1'.");
            }
        }

        [TestMethod]
        public void ArgumentParser_ExclusiveArgumentHierarchy_OK()
        {
            string[] args = new string[] { "/D1", "1", "/D4", "123456" };
            ExParentDataMock data = new ExParentDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);
            Assert.IsTrue(data.Data1 == 1);
            Assert.IsTrue(data.Data4 == 123456);
        }

        [TestMethod]
        public void ArgumentParser_ExcluArgHierarchy_2ExclusivesDiffGroup()
        {
            string[] args = new string[] { "/D1", "1", "/D3", "3" };
            ExParentDataMock data = new ExParentDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);
            Assert.IsTrue(data.Data1 == 1);
            Assert.IsTrue(data.Data3 == 3);
        }

        [TestMethod]
        public void ArgumentParser_ExcluArgHierarchy_2ExclusivesDiffGroupOther()
        {
            string[] args = new string[] { "/D1", "1", "/D3", "3", "/D4", "4" };
            ExParentDataMock data = new ExParentDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A ExclusiveArgumentException exception must be thrown.");
            }
            catch (ExclusiveArgumentException e)
            {
                // D3 is in group 0 (not specified), so it cannot be used with D4.
                // D1 & D2 are in group 1
                Assert.IsTrue(e.Message == "The argument '/D3' cannot be used in conjonction with '/D4'.");
            }
        }

        [TestMethod]
        public void ArgumentParser_ExcluArgHierarchy_2ExclusivesSameGroup()
        {
            string[] args = new string[] { "/D1", "1", "/D2", "2" };
            ExParentDataMock data = new ExParentDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A ExclusiveArgumentException exception must be thrown.");
            }
            catch (ExclusiveArgumentException e)
            {
                // D1 & D2 are in group 1
                Assert.IsTrue(e.Message == "The argument '/D2' cannot be used in conjonction with '/D1'.");
            }
        }

        [TestMethod]
        public void ArgumentParser_ExcluArgHierarchy_2ExclusivesDiffLevel()
        {
            string[] args = new string[] { "/D1", "1", "/CHILD", "/DC1", "1" };
            ExParentDataMock data = new ExParentDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);
            Assert.IsTrue(data.Data1 == 1);
            Assert.IsNotNull(data.Child);
            Assert.IsTrue(data.Child.DataC1 == 1);
        }

        [TestMethod]
        public void ArgumentParser_ExcluArgHierarchy_3ExclusivesDiffLevel()
        {
            string[] args = new string[] { "/D1", "1", "/CHILD", "/DC1", "1", "/DC2", "2" };
            ExParentDataMock data = new ExParentDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A ExclusiveArgumentException exception must be thrown.");
            }
            catch (ExclusiveArgumentException e)
            {
                Assert.IsTrue(e.Message == "The argument '/DC2' cannot be used in conjonction with '/DC1'.");
            }
        }

        [TestMethod]
        public void ArgumentParser_ExcluArgHierarchy_3ExclusivesDiffLevelOK()
        {
            string[] args = new string[] { "/D1", "1", "/CHILD", "/DC1", "1", "/DC3", "3" };
            ExParentDataMock data = new ExParentDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);
            Assert.IsTrue(data.Data1 == 1);
            Assert.IsNotNull(data.Child);
            Assert.IsTrue(data.Child.DataC1 == 1);
            Assert.IsTrue(data.Child.DataC3 == 3);
        }

        [TestMethod]
        public void ArgumentParser_MandatoryArgumentGroup_OK1()
        {
            string[] args = new string[] { "-ARG1", "1", "-ARG3", "3" };
            MandatoryGroupDataMock data = new MandatoryGroupDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsTrue(data.Arg1 == 1);
            Assert.IsTrue(data.Arg2 == 0);
            Assert.IsTrue(data.Arg3 == 3);
        }

        [TestMethod]
        public void ArgumentParser_MandatoryArgumentGroup_OK2()
        {
            string[] args = new string[] { "-ARG2", "2", "-ARG3", "3" };
            MandatoryGroupDataMock data = new MandatoryGroupDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            parser.Parse(data, args);

            Assert.IsTrue(data.Arg1 == 0);
            Assert.IsTrue(data.Arg2 == 2);
            Assert.IsTrue(data.Arg3 == 3);
        }

        [TestMethod]
        public void ArgumentParser_MandatoryArgumentGroup_Missing()
        {
            string[] args = new string[] { "-ARG3", "3" };
            MandatoryGroupDataMock data = new MandatoryGroupDataMock();

            ArgumentsParser parser = new ArgumentsParser();
            try
            {
                parser.Parse(data, args);
                Assert.Fail("A ExclusiveArgumentException exception must be thrown.");
            }
            catch (GroupedMandatoryArgumentException e)
            {
                Assert.IsTrue(e.Message == "One of the following arguments is missing: -ARG1, -ARG2");
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
