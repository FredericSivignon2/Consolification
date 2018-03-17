using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class ArgumentsParser
    {
        private ArgumentInfoCollection argumentsInfo = new ArgumentInfoCollection();
        private object data;
        private int currentArgIndex = 0;
        private int currentSimpleArgumentIndex = 0;

        #region Public Properties
        public ArgumentInfoCollection ArgumentsInfo
        {
            get { return this.argumentsInfo; }
        }

        public bool MustDisplayHelp { get; private set; } = false;
        public string CommandDescription { get; private set; }
        public CIJobAttribute MainJob { get; private set; }
        public IPasswordReader PasswordReader { get; set; }
        public IConsoleWrapper Console { get; set; }
        #endregion

        public ArgumentsParser()
        {
            Console = new DefaultConsoleWrapper();
            PasswordReader = new DefaultPasswordReader(Console);
        }

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="container"></param>
        public void Parse(object data, string[] args)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (args == null)
                throw new ArgumentNullException("args");

            this.data = data;
            Type type = data.GetType();

            try
            {
                ProcessClassAttributes(type, args);
                RegisterAttributesFromClassProperties(type, this.argumentsInfo);
                SetPropertiesValuesFromAttributes(args, this.argumentsInfo, data);

                ArgumentsParserValidator validator = new ArgumentsParserValidator(this, data);
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
        
        private void ProcessClassAttributes(Type type, string[] args)
        {
            CIHelpArgumentAttribute chta = type.GetCustomAttribute<CIHelpArgumentAttribute>();
            if (chta != null)
            {
                MustDisplayHelp = !args.All(name => !(chta.Name == name));
                ArgumentInfo ainfo = new ArgumentInfo(chta);
                argumentsInfo.Add(ainfo);
            }

            CICommandDescriptionAttribute cda = type.GetCustomAttribute<CICommandDescriptionAttribute>();
            if (cda != null)
            {
                CommandDescription = cda.Description;
            }

            MainJob = type.GetCustomAttribute<CIJobAttribute>();
        }

        private static void RegisterAttributesFromClassProperties(Type type, ArgumentInfoCollection argumentsInfo)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pinfo in properties)
            {
                ArgumentInfo ainfo = null;
                CISimpleArgumentAttribute csaa = pinfo.GetCustomAttribute<CISimpleArgumentAttribute>();
                if (csaa == null)
                {
                    CINamedArgumentAttribute cnaa = pinfo.GetCustomAttribute<CINamedArgumentAttribute>();
                    if (cnaa == null)
                        continue; // Not a field associated with a console argument

                    if (cnaa.Names.Length == 0)
                        throw new InvalidArgumentDefinitionException(string.Format("No argument name associated with the property {0}.", pinfo.Name));

                    // If the collection already contains one the of arguments specified in the current AppArgumentAttribute
                    if (argumentsInfo.Contains(cnaa.Names))
                    {
                        throw new InvalidArgumentDefinitionException(string.Format("One of the argument specified with the property {0} has been already registered.", pinfo.Name));
                    }
                    ainfo = new ArgumentInfo(cnaa);
                }
                else
                {
                    ainfo = new ArgumentInfo(csaa);
                }
               
                ainfo.PInfo = pinfo;
                ainfo.MandatoryArguments = pinfo.GetCustomAttribute<CIMandatoryArgumentAttribute>();
                ainfo.ArgumentBoundary = pinfo.GetCustomAttribute<CIArgumentBoundaryAttribute>();
                ainfo.Job = pinfo.GetCustomAttribute<CIJobAttribute>();
                ainfo.ChildArgument = pinfo.GetCustomAttribute<CIChildArgumentAttribute>();
                ainfo.ParentArgument = pinfo.GetCustomAttribute<CIParentArgumentAttribute>();
                ainfo.FileContent = pinfo.GetCustomAttribute<CIFileContentAttribute>();
                ainfo.Password = pinfo.GetCustomAttribute<CIPasswordAttribute>();

                if (pinfo.PropertyType.IsUserType())
                {
                    ainfo.UserType = true;
                    ainfo.UserTypeInstance = Activator.CreateInstance(pinfo.PropertyType);

                    ArgumentInfoCollection childrenInfo = new ArgumentInfoCollection();
                    RegisterAttributesFromClassProperties(pinfo.PropertyType, childrenInfo);
                    ainfo.Children.AddRange(childrenInfo);
                }

                argumentsInfo.Add(ainfo);
            }
        }

        private void SetPropertiesValuesFromAttributes(string[] args, ArgumentInfoCollection argumentsInfo, object data)
        {
            string argValue = "";

            while (this.currentArgIndex < args.Length)
            {
                string arg = args[this.currentArgIndex];
                ArgumentInfo currentInfo = null;

                // If the current argument is not registered as a valid argument name
                if (argumentsInfo.Contains(arg) == false)
                {
                    currentInfo = argumentsInfo.GetSimpleArgument(currentSimpleArgumentIndex);
                    if (currentInfo == null)
                    {
                        ArgumentInfo argInfo = this.argumentsInfo.GetParentArgument(arg);
                        if (argInfo != null)
                        {
                            throw new MissingParentArgumentException(argInfo.Name, arg);
                        }

                        if (argumentsInfo == this.argumentsInfo)
                            throw new UnknownArgumentException(arg);

                        // Not at this level, looks again at upper level
                        this.currentArgIndex--;
                        return;
                    }

                    argValue = arg;
                    currentSimpleArgumentIndex++;
                }
                else
                {
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
                        this.currentArgIndex++;
                        continue;
                    }

                    if (Type.GetTypeCode(currentInfo.PInfo.PropertyType) != TypeCode.Boolean &&
                        currentInfo.UserType == false)
                    {
                        if (this.currentArgIndex >= args.Length - 1)
                            throw new ArgumentException("Missing value for the argument {0}", arg);

                        argValue = args[++this.currentArgIndex];
                    }
                }

                currentInfo.Found = true;
                if (currentInfo.UserType)
                {
                    this.currentArgIndex++;
                    SetPropertiesValuesFromAttributes(args, currentInfo.Children, currentInfo.UserTypeInstance);

                    currentInfo.PInfo.SetValue(data, currentInfo.UserTypeInstance);
                }
                else
                {
                    currentInfo.SetPropertyValue(data, argValue);
                }
                this.currentArgIndex++;
            }
        }
        #endregion
    }
}
