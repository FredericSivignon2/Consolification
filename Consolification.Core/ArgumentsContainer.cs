using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public abstract class ArgumentsContainer
    {
        private ArgumentInfoCollection argumentsInfo = new ArgumentInfoCollection();

        #region Public Properties
        public ArgumentInfoCollection ArgumentsInfo
        {
            get { return this.argumentsInfo; }
        }

        public bool MustDisplayHelp { get; private set; } = false;
        public string CommandDescription { get; private set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="container"></param>
        public void Initialize(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            
            Type type = this.GetType();

            try
            {
                ProcessClassAttributes(type, args);
                RegisterAttributesFromClassProperties(type);
                SetPropertiesValuesFromAttributes(args);

                ArgumentsContainerValidator validator = new ArgumentsContainerValidator(this);
                validator.Validate();
            }
            catch
            {
                MustDisplayHelp = true;
                throw;
            }
        }

        #endregion

        #region Private Methods
        private T GetComparableValue<T>(string[] args, ref int index, ArgumentInfo info, Func<string, T> convertor) where T : IComparable
        {
            index++;

            if (index >= args.Length)
                throw new ArgumentException("Missing value for the argument {0}", args[index - 1]);
            T val = default(T);
            try
            {
                val = convertor(args[index]);
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("Invalid specified value for the argument {0}.", e));
            }

            if (info.ArgumentBoundary != null && !string.IsNullOrWhiteSpace(info.ArgumentBoundary.MinValue))
            {
                T minVal = convertor(info.ArgumentBoundary.MinValue);
                if (val.CompareTo(minVal) < 0)
                    throw new ArgumentException(string.Format("The value of the argument {0} cannot be lower than {1}", args[index - 1], minVal));

            }

            if (info.ArgumentBoundary != null && !string.IsNullOrWhiteSpace(info.ArgumentBoundary.MaxValue))
            {
                T maxVal = convertor(info.ArgumentBoundary.MaxValue);
                if (val.CompareTo(maxVal) > 0)
                    throw new ArgumentException(string.Format("The value of the argument {0} cannot be greater than {1}", args[index - 1], maxVal));

            }

            return val;

        }

        private T GetObjectValue<T>(string[] args, ref int index, ArgumentInfo info, Func<string, T> convertor)
        {
            index++;

            if (index >= args.Length)
                throw new ArgumentException("Missing value for the argument {0}", args[index - 1]);
            T val = default(T);
            try
            {
                val = convertor(args[index]);
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("Invalid specified value for the argument {0}.", e));
            }
            return val;
        }

        private void ProcessClassAttributes(Type type, string[] args)
        {
            CIHelpArgumentAttribute chta = type.GetCustomAttribute<CIHelpArgumentAttribute>();
            if (chta != null)
            {
                MustDisplayHelp = !args.All(name => !chta.Names.Contains<string>(name));
                ArgumentInfo ainfo = new ArgumentInfo(chta);
                argumentsInfo.Add(ainfo);
            }

            CICommandDescriptionAttribute cda = type.GetCustomAttribute<CICommandDescriptionAttribute>();
            if (cda != null)
            {
                CommandDescription = cda.Description;
            }
        }

        private void RegisterAttributesFromClassProperties(Type type)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pinfo in properties)
            {
                CIArgumentAttribute caa = pinfo.GetCustomAttribute<CIArgumentAttribute>();
                if (caa == null)
                    continue; // Not a field associated with a console argument

                if (caa.Names.Length == 0)
                    throw new InvalidArgumentDefinitionException(string.Format("No argument associated with the property {0}.", pinfo.Name));

                // If the collection already contains one the of arguments specified in the current AppArgumentAttribute
                if (argumentsInfo.Contains(caa.Names))
                {
                    throw new InvalidArgumentDefinitionException(string.Format("One of the argument specified with the property {0} has been already registered.", pinfo.Name));
                }

                ArgumentInfo ainfo = new ArgumentInfo(caa);
                ainfo.PInfo = pinfo;

                ainfo.MandatoryArguments = pinfo.GetCustomAttribute<CIMandatoryArgumentAttribute>();
                ainfo.ArgumentBoundary = pinfo.GetCustomAttribute<CIArgumentBoundaryAttribute>();
                ainfo.Job = pinfo.GetCustomAttribute<CIJobAttribute>();
                ainfo.ChildArgument = pinfo.GetCustomAttribute<CIChildArgumentAttribute>();
                ainfo.ParentArgument = pinfo.GetCustomAttribute<CIParentArgumentAttribute>();

                //if (ainfo.C)

                argumentsInfo.Add(ainfo);
            }
        }

        private void SetPropertiesValuesFromAttributes(string[] args)
        {
            int index = 0;
            while (index < args.Length)
            {
                string arg = args[index];

                ArgumentInfo currentInfo = null;
                try
                {
                    currentInfo = argumentsInfo.FromName(arg);
                }
                catch (InvalidOperationException)
                {
                    throw new UnknownArgumentException(arg);
                }
                if (currentInfo.PInfo == null)
                {
                    index++;
                    continue;
                }
                currentInfo.Found = true;

                TypeCode code = Type.GetTypeCode(currentInfo.PInfo.PropertyType);
                switch (code)
                {

                    // Boolean values arguments do not have associated string values; if specified,
                    // just set to true the associated property.
                    case TypeCode.Boolean:
                        currentInfo.PInfo.SetValue(this, true);
                        break;

                    case TypeCode.Byte:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<byte>(args, ref index, currentInfo, (str) => { return Convert.ToByte(str); }));
                        break;

                    case TypeCode.SByte:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<sbyte>(args, ref index, currentInfo, (str) => { return Convert.ToSByte(str); }));
                        break;

                    case TypeCode.Char:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<char>(args, ref index, currentInfo, (str) => { return Convert.ToChar(str); }));
                        break;

                    case TypeCode.Decimal:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<decimal>(args, ref index, currentInfo, (str) => { return Convert.ToDecimal(str); }));
                        break;
                        
                    case TypeCode.Int16:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<short>(args, ref index, currentInfo, (str) => { return Convert.ToInt16(str); }));
                        break;

                    case TypeCode.Int32:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<int>(args, ref index, currentInfo, (str) => { return Convert.ToInt32(str); }));
                        break;

                    case TypeCode.Int64:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<long>(args, ref index, currentInfo, (str) => { return Convert.ToInt64(str); }));
                        break;

                    case TypeCode.UInt16:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<ushort>(args, ref index, currentInfo, (str) => { return Convert.ToUInt16(str); }));
                        break;

                    case TypeCode.UInt32:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<uint>(args, ref index, currentInfo, (str) => { return Convert.ToUInt32(str); }));
                        break;

                    case TypeCode.UInt64:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<ulong>(args, ref index, currentInfo, (str) => { return Convert.ToUInt64(str); }));
                        break;

                    case TypeCode.Single:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<float>(args, ref index, currentInfo, (str) => { return Convert.ToSingle(str, CultureInfo.InvariantCulture); }));
                        break;

                    case TypeCode.Double:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<double>(args, ref index, currentInfo, (str) => { return Convert.ToDouble(str, CultureInfo.InvariantCulture); }));
                        break;

                    case TypeCode.String:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<string>(args, ref index, currentInfo, (str) => { return str; }));
                        break;

                    case TypeCode.DateTime:
                        currentInfo.PInfo.SetValue(this, GetComparableValue<DateTime>(args, ref index, currentInfo, (str) => { return Convert.ToDateTime(str, CultureInfo.InvariantCulture); }));
                        break;

                    case TypeCode.Object:
                        SetValueForObjects(args, ref index, currentInfo);
                        break;

                    default:
                        throw new NotSupportedException(string.Format("The type of the argument '{0}' is not supported.", currentInfo.Argument.Name));
                }


                index++;
            }
        }

        private void SetValueForObjects(string[] args, ref int index, ArgumentInfo currentInfo)
        {
            switch (currentInfo.PInfo.PropertyType.FullName)
            {
                case "System.Uri":
                    currentInfo.PInfo.SetValue(this, GetObjectValue<Uri>(args, ref index, currentInfo, 
                        (str) => { return new Uri(str); }));
                    break;

                case "System.Version":
                    currentInfo.PInfo.SetValue(this, GetComparableValue<Version>(args, ref index, currentInfo,
                        (str) => { return new Version(str); }));
                    break;

                default:
                    throw new NotSupportedException(string.Format("The type of the argument '{0}' is not supported.", currentInfo.Argument.Name));

            }
        }
        #endregion
    }
}
