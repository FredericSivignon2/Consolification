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

        #region Public Properties
        public ArgumentInfoCollection ArgumentsInfo
        {
            get { return this.argumentsInfo; }
        }

        public bool MustDisplayHelp { get; private set; } = false;
        public string CommandDescription { get; private set; }
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
                RegisterAttributesFromClassProperties(type);
                SetPropertiesValuesFromAttributes(args);

                ArgumentsContainerValidator validator = new ArgumentsContainerValidator(this, data);
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
                ainfo.FileContent = pinfo.GetCustomAttribute<CIFileContentAttribute>();
                ainfo.Password = pinfo.GetCustomAttribute<CIPasswordAttribute>();

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

                string argValue = "";
                if (Type.GetTypeCode(currentInfo.PInfo.PropertyType) != TypeCode.Boolean)
                {
                    if (index >= args.Length - 1)
                        throw new ArgumentException("Missing value for the argument {0}", arg);

                    argValue = args[++index];
                }
               
                currentInfo.Found = true;

                currentInfo.SetPropertyValue(this.data, argValue);
                
                index++;
            }
        }
        #endregion
    }
}
