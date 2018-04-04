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
                SetPropertiesValuesFromArguments(args, this.argumentsInfo, data);

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

        private void RegisterAttributesFromClassProperties(Type type, ArgumentInfoCollection argumentsInfo)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int simpleArgumentIndex = 0;

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
                    ainfo = new ArgumentInfo(csaa, simpleArgumentIndex++);
                }
               
                ainfo.PInfo = pinfo;
                ainfo.MandatoryArgument = pinfo.GetCustomAttribute<CIMandatoryArgumentAttribute>();
                ainfo.GroupedMandatoryArgument = pinfo.GetCustomAttribute<CIGroupedMandatoryArgumentAttribute>();
                ainfo.ArgumentBoundary = pinfo.GetCustomAttribute<CIArgumentBoundaryAttribute>();
                ainfo.ArgumentValueLength = pinfo.GetCustomAttribute<CIArgumentValueLengthAttribute>();
                ainfo.ArgumentFormat = pinfo.GetCustomAttribute<CIArgumentFormatAttribute>();
                ainfo.Job = pinfo.GetCustomAttribute<CIJobAttribute>();
                ainfo.FileContent = pinfo.GetCustomAttribute<CIFileContentAttribute>();
                ainfo.Password = pinfo.GetCustomAttribute<CIPasswordAttribute>();
                ainfo.Exclusive = pinfo.GetCustomAttribute<CIExclusiveArgumentAttribute>();

                if (pinfo.PropertyType.IsUserType())
                {
                    ainfo.UserType = true;
                    ainfo.UserTypeInstance = Activator.CreateInstance(pinfo.PropertyType);

                    ArgumentInfoCollection childrenInfo = new ArgumentInfoCollection();
                    RegisterAttributesFromClassProperties(pinfo.PropertyType, childrenInfo);
                    ainfo.Children.AddRange(childrenInfo);
                }

                if (ainfo.NamedArgument != null &&
                     this.argumentsInfo.DeepContains(ainfo.Name))
                {
                    throw new DuplicateArgumentDefinitionException(ainfo.Name);
                }

                argumentsInfo.Add(ainfo);
            }
        }

        private void SetPropertiesValuesFromArguments(string[] args, ArgumentInfoCollection argumentsInfo, object data)
        {
            string argValue = "";
            int currentSimpleArgumentIndex = 0;
            ArgumentExclusivityController exController = new ArgumentExclusivityController();

            while (this.currentArgIndex < args.Length)
            {
                string arg = args[this.currentArgIndex];
                ArgumentInfo currentInfo = null;

                // If the current argument is not registered as a valid argument name
                // within the current argument information collection...
                if (argumentsInfo.Contains(arg) == false)
                {
                    // ... Try to see if there is a simple argument for this collection
                    currentInfo = argumentsInfo.GetSimpleArgument(currentSimpleArgumentIndex);
                    if (currentInfo == null)
                    {
                        // If not, search for the parent argument
                        ArgumentInfo argInfo = this.argumentsInfo.GetParentArgument(arg);
                        if (argInfo != null && argInfo.Found == false)
                        {
                            // There is a parent argument, but it has not been already found
                            if (args.Contains<string>(argInfo.Name))
                                throw new WrongArgumentPositionException(argInfo.Name, arg);
                            else
                                throw new MissingParentArgumentException(argInfo.Name, arg);
                        }
                        // If the argument does not exist in the entire hierarchy...
                        if (this.argumentsInfo.DeepContains(arg) == false)
                        {
                            throw new UnknownArgumentException(arg);
                        }

                        // Not at this level, looks again at upper level
                        this.currentArgIndex--;
                        return;
                    }
                    else
                    {
                        // For a simple argument, the value cannot be the name of another defined argument.
                        if (this.argumentsInfo.DeepContains(arg) && currentInfo.MandatoryArgument != null)
                        {
                            throw new MissingMandatoryArgumentException(currentInfo.SimpleArgument.HelpText);
                        }
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
                    exController.NewArgument(currentInfo);

                    if (currentInfo.PInfo == null)
                    {
                        this.currentArgIndex++;
                        continue;
                    }

                    if (Type.GetTypeCode(currentInfo.PInfo.PropertyType) != TypeCode.Boolean &&
                        currentInfo.UserType == false)
                    {
                        if (this.currentArgIndex >= args.Length - 1)
                            throw new MissingArgumentValueException(arg);

                        // If the value is a known argument, it means the value is missing!
                        string value = args[++this.currentArgIndex];
                        if (this.argumentsInfo.DeepContains(value))
                        {
                            throw new MissingArgumentValueException(arg);
                        }

                        argValue = value;
                    }
                }

                currentInfo.Found = true;
                // If the current property info represent a user defined type
                if (currentInfo.UserType)
                {
                    this.currentArgIndex++;
                    // Set values for this user defined type
                    SetPropertiesValuesFromArguments(args, currentInfo.Children, currentInfo.UserTypeInstance);
                    // Associates the user defined type instance to the parent class instance
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
