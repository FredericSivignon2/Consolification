using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class ConsolificationEngine
    {
        private class ArgumentInfo
        {
            public PropertyInfo PInfo { get; set; }
            public AppArgumentAttribute Argument { get; set; }
            public AppMandatoryArgumentAttribute MandatoryArguments { get; set; }
            public bool MandatoryFound { get; set; }
        }

        private Dictionary<string, ArgumentInfo> argumentsInfo = new Dictionary<string, ArgumentInfo>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="container"></param>
        public ConsolificationEngine(string[] args, IArgumentsContainer container)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (container == null)
                throw new ArgumentNullException("container");

            Type type = container.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo pinfo in properties)
            {
                AppArgumentAttribute caa = pinfo.GetCustomAttribute<AppArgumentAttribute>();
                if (caa == null)
                    continue; // Not a field associated with a console argument

                if (caa.Names.Length == 0)
                    throw new InvalidOperationException(string.Format("No argument associated with the property {0}.", pinfo.Name));

                // If the dictionary already contains one the of arguments specified in the current AppArgumentAttribute
                if (caa.Names.Any() && caa.Names.All(key => argumentsInfo.ContainsKey(key)))
                {
                    throw new InvalidOperationException(string.Format("One of the argument specified for the property {0} has been already registered.", pinfo.Name));
                }

                ArgumentInfo ainfo = new ArgumentInfo()
                {
                    PInfo = pinfo,
                    Argument = caa
                };


                AppMandatoryArgumentAttribute cmaa = pinfo.GetCustomAttribute<AppMandatoryArgumentAttribute>();
                if (cmaa != null)
                {
                    ArgumentInfo mainfo = new ArgumentInfo()
                    {
                        PInfo = pinfo,
                        MandatoryArguments = cmaa
                    };

                    utiliser MandatoryFound

                    mandatoryArguments.Add(caa.Name, mainfo);
                }


                foreach (string argName in caa.Names)
                {
                    argumentsInfo.Add(argName, ainfo);
                }

            }

            int index = 0;
            while (index < args.Length)
            {
                string arg = args[index];
                if (mandatoryArguments.Contains<string>(arg))
                    mandatoryArguments.Remove(arg);

                ArgumentInfo currentInfo = null;
                if (argumentsInfo.TryGetValue(arg, out currentInfo))
                {
                    switch (Type.GetTypeCode(currentInfo.PInfo.PropertyType))
                    {

                        // Boolean values arguments do not have associated string values; if specified,
                        // just set to true the associated property.
                        case TypeCode.Boolean:
                            currentInfo.PInfo.SetValue(container, true);
                            break;

                        case TypeCode.Byte:
                            currentInfo.PInfo.SetValue(container, GetValue<byte>(args, ref index, currentInfo, (str) => { return Convert.ToByte(str); }));
                            break;

                        case TypeCode.Int16:
                            currentInfo.PInfo.SetValue(container, GetValue<short>(args, ref index, currentInfo, (str) => { return Convert.ToInt16(str); }));
                            break;

                        case TypeCode.Int32:
                            currentInfo.PInfo.SetValue(container, GetValue<int>(args, ref index, currentInfo, (str) => { return Convert.ToInt32(str); }));
                            break;

                        case TypeCode.Int64:
                            currentInfo.PInfo.SetValue(container, GetValue<long>(args, ref index, currentInfo, (str) => { return Convert.ToInt64(str); }));
                            break;

                        case TypeCode.Double:
                            currentInfo.PInfo.SetValue(container, GetValue<double>(args, ref index, currentInfo, (str) => { return Convert.ToDouble(str, CultureInfo.InvariantCulture); }));
                            break;

                        case TypeCode.String:
                            currentInfo.PInfo.SetValue(container, GetValue<string>(args, ref index, currentInfo, (str) => { return str; }));
                            break;

                    }
                }

                index++;
            }

            if (mandatoryArguments.Count > 0)
            {
                string firstMissingArgument = mandatoryArguments[0];
                throw new MissingArgumentException(firstMissingArgument);
            }
        }


        private T GetValue<T>(string[] args, ref int index, ArgumentInfo info, Func<string, T> convertor) where T : IComparable
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

            if (!string.IsNullOrWhiteSpace(info.Argument.MinValue))
            {
                T minVal = convertor(info.Argument.MinValue);
                if (val.CompareTo(minVal) < 0)
                    throw new ArgumentException(string.Format("The value of the argument {0} cannot be lower than {1}", args[index - 1], minVal));

            }

            if (!string.IsNullOrWhiteSpace(info.Argument.MaxValue))
            {
                T maxVal = convertor(info.Argument.MaxValue);
                if (val.CompareTo(maxVal) > 0)
                    throw new ArgumentException(string.Format("The value of the argument {0} cannot be greater than {1}", args[index - 1], maxVal));

            }

            return val;

        }
    }
}
