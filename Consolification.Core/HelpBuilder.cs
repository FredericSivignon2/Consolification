using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    class HelpBuilder
    {
        private ArgumentsParser container;
        
        public HelpBuilder(ArgumentsParser container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.container = container;
        }

        public string[] GetHelpLines()
        {
            List<string> lines = new List<string>();

            lines.AddRange(GetHeaderLines());
            lines.AddRange(GetArgumentsDescriptionLines());

           

            return lines.ToArray();
        }


        private string[] GetHeaderLines()
        {
            List<string> lines = new List<string>();

            StringBuilder usage = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(this.container.CommandDescription))
            {
                lines.Add(this.container.CommandDescription);
                lines.Add(string.Empty);
            }


            usage.Append("Usage: ");
            usage.Append(GetExeName());

            AppendHeaderArgs(usage, this.container.ArgumentsInfo.Hierarchy);

            lines.Add(usage.ToString());
            lines.Add(string.Empty);

            return lines.ToArray();
        }

        private void AppendHeaderArgs(StringBuilder usage, ArgumentInfo[] hierarchy)
        { 
            foreach (ArgumentInfo argInfo in hierarchy)
            {
                usage.Append(" ");
                if (argInfo.MandatoryArguments != null)
                {
                    usage.Append(argInfo.Argument.Names[0]);
                    AppendHeaderArgs(usage, argInfo.Children.ToArray<ArgumentInfo>());
                }
                else
                {
                    usage.AppendFormat("[{0}", argInfo.Argument.Names[0]);
                    AppendHeaderArgs(usage, argInfo.Children.ToArray<ArgumentInfo>());
                    usage.Append("]");
                }
            }
            
        }

        private string GetExeName()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }
            return assembly.GetName().Name;
        }

        private string[] GetArgumentsDescriptionLines()
        {
            List<string> lines = new List<string>();
            
            AddArgumentDescriptionLines(lines, this.container.ArgumentsInfo.Hierarchy);

            return lines.ToArray();
        }

        private void AddArgumentDescriptionLines(List<string> lines, ArgumentInfo[] argsInfo)
        {
            if (argsInfo.Length == 0)
                return;

            int maxArgNamesLength = this.container.ArgumentsInfo.MaxArgumentsStringLength;
            foreach (ArgumentInfo argInfo in argsInfo)
            {
                if (string.IsNullOrWhiteSpace(argInfo.Argument.Description))
                    continue;

                string line = GetHelpLineFromArgument(argInfo, maxArgNamesLength);

                lines.Add(line);
                AddArgumentDescriptionLines(lines, argInfo.Children.ToArray<ArgumentInfo>());
            }
            
        }

        private string GetHelpLineFromArgument(ArgumentInfo argInfo, int maxArgNamesLength)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string name in argInfo.Argument.Names)
            {
                if (builder.Length > 0)
                    builder.Append(", ");

                builder.Append(name);
            }

            int spaceLeft = maxArgNamesLength - argInfo.Argument.NamesLength;
            for (int i = 0; i < spaceLeft + 1; i++)
            {
                builder.Append(" ");
            }

            builder.Append(argInfo.Argument.Description);

            return builder.ToString();
        }
    }
}
