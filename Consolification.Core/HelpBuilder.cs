using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class HelpBuilder
    {
        private ArgumentsContainer container;
        
        public HelpBuilder(ArgumentsContainer container)
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
                }
                else
                {
                    usage.AppendFormat("[{0}]", argInfo.Argument.Names[0]);
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
            int maxArgNamesLength = this.container.ArgumentsInfo.MaxArgumentsStringLength;
            foreach (ArgumentInfo argInfo in this.container.ArgumentsInfo)
            {
                if (string.IsNullOrWhiteSpace(argInfo.Argument.Description))
                    continue;

                string line = GetHelpLineFromArgument(argInfo, maxArgNamesLength);
                lines.Add(line);
            }

            return lines.ToArray();
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
