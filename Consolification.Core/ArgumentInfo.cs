using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    /// <summary>
    /// Maintains information related to an argument.
    /// </summary>
    public class ArgumentInfo
    {
        #region Constructors
        public ArgumentInfo(CISimpleArgumentAttribute argument, int index)
        {
            if (index < 0)
                throw new ArgumentException("The index must be greater or equal to zero.");
            if (argument == null)
                throw new ArgumentNullException("argument");

            SimpleArgument = argument;
            SimpleArgumentIndex = index;
        }

        public ArgumentInfo(CINamedArgumentAttribute argument)
        {
            if (argument == null)
                throw new ArgumentNullException("argument");

            NamedArgument = argument;
        }
        #endregion

        #region Public Properties
        public CISimpleArgumentAttribute SimpleArgument { get; private set; }
        public int SimpleArgumentIndex { get; private set; }
        public CINamedArgumentAttribute NamedArgument { get; private set; }
        public PropertyInfo PInfo { get; set; }
        public CIMandatoryArgumentAttribute MandatoryArgument { get; set; }
        public CIArgumentBoundaryAttribute ArgumentBoundary { get; set; }
        public CIJobAttribute Job { get; set; }
        public CIChildArgumentAttribute ChildArgument { get; set; }
        public CIParentArgumentAttribute ParentArgument { get; set; }
        public CIFileContentAttribute FileContent { get; set; }
        public CIPasswordAttribute Password { get; set; }
        public bool Found { get; set; }

        public bool UserType { get; set; }

        public object UserTypeInstance { get; set; }

        public ArgumentInfoCollection Children { get; } = new ArgumentInfoCollection();

        public string Name
        {
            get
            {
                if (NamedArgument != null)
                    return NamedArgument.Name;

                return string.Empty;
            }
        }
        #endregion

        #region Overridden
        public override string ToString()
        {
            StringBuilder output = new StringBuilder("Argument: ");

            if (SimpleArgument != null)
            {
                output.Append(SimpleArgument.HelpText);
            }
            else
            {
                output.Append(NamedArgument.Name);

            }
            output.AppendFormat(" - Found: {0}", Found);

            return output.ToString();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the given value to the corresponding property within the object 'data'.
        /// </summary>
        /// <param name="data">The object for which we need to set the property value.</param>
        /// <param name="argValue">The value of the property to set.</param>
        public void SetPropertyValue(object data, string argValue)
        {
            if (FileContent != null)
            {
                SetFileContentPropertyValue(data, argValue);
            }
            else
            {
                SetDirectPropertyValue(data, argValue);
            }
        }
        #endregion

        #region Private Methods
        
        private void SetFileContentPropertyValue(object data, string argValue)
        {
            switch (PInfo.PropertyType.FullName)
            {
                case "System.String":
                    PInfo.SetValue(data, GetObjectValue<string>(argValue,
                       (str) =>
                       {
                           if (File.Exists(str) == false)
                           {
                               throw new FileNotFoundException(string.Format("The file path specified with the argument '{0}' does not exist.", NamedArgument.Name), str);
                           }

                           return File.ReadAllText(str, FileContent.Encoding);
                       }));
                    break;

                case "System.String[]":
                    PInfo.SetValue(data, GetObjectValue<string[]>(argValue, 
                       (str) =>
                       {
                           if (File.Exists(str) == false)
                           {
                               throw new FileNotFoundException(string.Format("The file path specified with the argument '{0}' does not exist.", NamedArgument.Name), str);
                           }

                           return File.ReadAllLines(str, FileContent.Encoding);
                       }));
                    break;

                case "System.Byte[]":
                    PInfo.SetValue(data, GetObjectValue<byte[]>(argValue, 
                        (str) =>
                        {
                            if (File.Exists(str) == false)
                            {
                                throw new FileNotFoundException(string.Format("The file path specified with the argument '{0}' does not exist.", NamedArgument.Name), str);
                            }
                            return File.ReadAllBytes(str);
                        }));
                    break;

                case "System.Char[]":
                    PInfo.SetValue(data, GetObjectValue<char[]>(argValue, 
                    (str) =>
                    {
                        if (File.Exists(str) == false)
                        {
                            throw new FileNotFoundException(string.Format("The file path specified with the argument '{0}' does not exist.", NamedArgument.Name), str);
                        }

                        return File.ReadAllText(str, FileContent.Encoding).ToArray<char>();
                    }));
                    break;

                case "System.IO.FileStream":
                    PInfo.SetValue(data, GetObjectValue<FileStream>(argValue, 
                    (str) =>
                    {
                        if (File.Exists(str) == false)
                        {
                            throw new FileNotFoundException(string.Format("The file path specified with the argument '{0}' does not exist.", NamedArgument.Name), str);
                        }

                        return File.OpenRead(str);
                    }));
                    break;

                default:
                    throw new NotSupportedException(string.Format("The type of the argument '{0}' is not supported when associated with the CIFileContentAttribute attribute.", NamedArgument.Name));

            }
        }

        private void SetDirectPropertyValue(object data, string argValue)
        {
            TypeCode code = Type.GetTypeCode(PInfo.PropertyType);
            switch (code)
            {

                // Boolean values arguments do not have associated string values; if specified,
                // just set to true the associated property.
                case TypeCode.Boolean:
                    PInfo.SetValue(data, true);
                    break;

                case TypeCode.Byte:
                    PInfo.SetValue(data, GetComparableValue<byte>(argValue, (str) => { return Convert.ToByte(str); }));
                    break;

                case TypeCode.SByte:
                    PInfo.SetValue(data, GetComparableValue<sbyte>(argValue, (str) => { return Convert.ToSByte(str); }));
                    break;

                case TypeCode.Char:
                    PInfo.SetValue(data, GetComparableValue<char>(argValue, (str) => { return Convert.ToChar(str); }));
                    break;

                case TypeCode.Decimal:
                    PInfo.SetValue(data, GetComparableValue<decimal>(argValue, (str) => { return Convert.ToDecimal(str); }));
                    break;

                case TypeCode.Int16:
                    PInfo.SetValue(data, GetComparableValue<short>(argValue, (str) => { return Convert.ToInt16(str); }));
                    break;

                case TypeCode.Int32:
                    PInfo.SetValue(data, GetComparableValue<int>(argValue, (str) => { return Convert.ToInt32(str); }));
                    break;

                case TypeCode.Int64:
                    PInfo.SetValue(data, GetComparableValue<long>(argValue, (str) => { return Convert.ToInt64(str); }));
                    break;

                case TypeCode.UInt16:
                    PInfo.SetValue(data, GetComparableValue<ushort>(argValue, (str) => { return Convert.ToUInt16(str); }));
                    break;

                case TypeCode.UInt32:
                    PInfo.SetValue(data, GetComparableValue<uint>(argValue, (str) => { return Convert.ToUInt32(str); }));
                    break;

                case TypeCode.UInt64:
                    PInfo.SetValue(data, GetComparableValue<ulong>(argValue, (str) => { return Convert.ToUInt64(str); }));
                    break;

                case TypeCode.Single:
                    PInfo.SetValue(data, GetComparableValue<float>(argValue, (str) => { return Convert.ToSingle(str, CultureInfo.InvariantCulture); }));
                    break;

                case TypeCode.Double:
                    PInfo.SetValue(data, GetComparableValue<double>(argValue, (str) => { return Convert.ToDouble(str, CultureInfo.InvariantCulture); }));
                    break;

                case TypeCode.String:
                    PInfo.SetValue(data, GetComparableValue<string>(argValue, (str) => { return str; }));
                    break;

                case TypeCode.DateTime:
                    PInfo.SetValue(data, GetComparableValue<DateTime>(argValue, (str) => { return Convert.ToDateTime(str, CultureInfo.InvariantCulture); }));
                    break;

                case TypeCode.Object:
                    SetValueForObjects(data, argValue);
                    break;

                default:
                    throw new NotSupportedException(string.Format("The type of the argument '{0}' is not supported.", NamedArgument.Name));
            }
        }

        // To process all types that are not defined in System.TypeCode
        private void SetValueForObjects(object data, string argValue)
        {
            switch (PInfo.PropertyType.FullName)
            {
                case "System.Uri":
                    PInfo.SetValue(data, GetObjectValue<Uri>(argValue,
                        (str) => { return new Uri(str); }));
                    break;

                case "System.Version":
                    PInfo.SetValue(data, GetComparableValue<Version>(argValue,
                        (str) => { return new Version(str); }));
                    break;

                case "System.Byte[]":
                    PInfo.SetValue(data, GetObjectValue<byte[]>(argValue,
                        (str) => { return Encoding.ASCII.GetBytes(str); }));
                    break;

                case "System.Char[]":
                    PInfo.SetValue(data, GetObjectValue<char[]>(argValue,
                        (str) => { return str.ToArray<char>(); }));
                    break;

                default:
                    throw new NotSupportedException(string.Format("The type of the argument '{0}' is not supported.", NamedArgument.Name));

            }
        }

        private T GetComparableValue<T>(string argValue, Func<string, T> convertor) where T : IComparable
        {
            T val = default(T);
            try
            {
                val = convertor(argValue);
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("Invalid specified value for the argument {0}.", e));
            }

            if (ArgumentBoundary != null && !string.IsNullOrWhiteSpace(ArgumentBoundary.MinValue))
            {
                T minVal = convertor(ArgumentBoundary.MinValue);
                if (val.CompareTo(minVal) < 0)
                    throw new ArgumentException(string.Format("The value of the argument {0} cannot be lower than {1}", NamedArgument.Name, minVal));

            }

            if (ArgumentBoundary != null && !string.IsNullOrWhiteSpace(ArgumentBoundary.MaxValue))
            {
                T maxVal = convertor(ArgumentBoundary.MaxValue);
                if (val.CompareTo(maxVal) > 0)
                    throw new ArgumentException(string.Format("The value of the argument {0} cannot be greater than {1}", NamedArgument.Name, maxVal));

            }

            return val;

        }

        private T GetObjectValue<T>(string argValue, Func<string, T> convertor)
        {
            T val = default(T);
            try
            {
                val = convertor(argValue);
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("Invalid specified value for the argument {0}.", NamedArgument.Name), e);
            }
            return val;
        }
        #endregion
    }
}
