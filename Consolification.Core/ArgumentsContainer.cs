﻿using Consolification.Core.Attributes;
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
        
        public ArgumentInfoCollection ArgumentsInfo
        {
            get { return this.argumentsInfo; }
        }

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
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pinfo in properties)
            {
                CIArgumentAttribute caa = pinfo.GetCustomAttribute<CIArgumentAttribute>();
                if (caa == null)
                    continue; // Not a field associated with a console argument

                if (caa.Names.Length == 0)
                    throw new InvalidOperationException(string.Format("No argument associated with the property {0}.", pinfo.Name));

                // If the collection already contains one the of arguments specified in the current AppArgumentAttribute
                if (argumentsInfo.Contains(caa.Names))
                {
                    throw new InvalidOperationException(string.Format("One of the argument specified for the property {0} has been already registered.", pinfo.Name));
                }

                ArgumentInfo ainfo = new ArgumentInfo()
                {
                    PInfo = pinfo,
                    Argument = caa
                };


                CIMandatoryArgumentAttribute cmaa = pinfo.GetCustomAttribute<CIMandatoryArgumentAttribute>();
                if (cmaa != null)
                {
                    ainfo.MandatoryArguments = cmaa;
                }

                CIArgumentBoundaryAttribute aba = pinfo.GetCustomAttribute<CIArgumentBoundaryAttribute>();
                if (aba != null)
                {
                    ainfo.ArgumentBoundary = aba;
                }

                CIJobAttribute ja = pinfo.GetCustomAttribute<CIJobAttribute>();
                if (ja != null)
                {
                    ainfo.Job = ja;
                }

                argumentsInfo.Add(ainfo);
            }

            int index = 0;
            while (index < args.Length)
            {
                string arg = args[index];

                ArgumentInfo currentInfo = argumentsInfo.FromName(arg);
                currentInfo.Found = true;

                switch (Type.GetTypeCode(currentInfo.PInfo.PropertyType))
                {

                    // Boolean values arguments do not have associated string values; if specified,
                    // just set to true the associated property.
                    case TypeCode.Boolean:
                        currentInfo.PInfo.SetValue(this, true);
                        break;

                    case TypeCode.Byte:
                        currentInfo.PInfo.SetValue(this, GetValue<byte>(args, ref index, currentInfo, (str) => { return Convert.ToByte(str); }));
                        break;

                    case TypeCode.Int16:
                        currentInfo.PInfo.SetValue(this, GetValue<short>(args, ref index, currentInfo, (str) => { return Convert.ToInt16(str); }));
                        break;

                    case TypeCode.Int32:
                        currentInfo.PInfo.SetValue(this, GetValue<int>(args, ref index, currentInfo, (str) => { return Convert.ToInt32(str); }));
                        break;

                    case TypeCode.Int64:
                        currentInfo.PInfo.SetValue(this, GetValue<long>(args, ref index, currentInfo, (str) => { return Convert.ToInt64(str); }));
                        break;

                    case TypeCode.Double:
                        currentInfo.PInfo.SetValue(this, GetValue<double>(args, ref index, currentInfo, (str) => { return Convert.ToDouble(str, CultureInfo.InvariantCulture); }));
                        break;

                    case TypeCode.String:
                        currentInfo.PInfo.SetValue(this, GetValue<string>(args, ref index, currentInfo, (str) => { return str; }));
                        break;

                }


                index++;
            }

            string argNotFound = argumentsInfo.MandatoryNotFound();
            if (argNotFound != null)
            {
                throw new MissingArgumentException(argNotFound);
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
    }
}
