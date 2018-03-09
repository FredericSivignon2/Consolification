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
        private ArgumentsParser parser;
        
        public HelpBuilder(ArgumentsParser parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            this.parser = parser;
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

            if (!string.IsNullOrWhiteSpace(this.parser.CommandDescription))
            {
                lines.Add(this.parser.CommandDescription);
                lines.Add(string.Empty);
            }


            usage.Append("Usage: ");
            usage.Append(GetExeName());

            AppendHeaderArgs(usage, this.parser.ArgumentsInfo.Hierarchy);

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
                    if (argInfo.SimpleArgument != null)
                        usage.Append(argInfo.SimpleArgument.HelpText);
                    else
                    {
                        if (string.IsNullOrWhiteSpace(argInfo.NamedArgument.ValueHelpText))
                            usage.Append(argInfo.NamedArgument.Names[0]);
                        else
                            usage.AppendFormat("{0} <{1}>", argInfo.NamedArgument.Names[0], argInfo.NamedArgument.ValueHelpText);
                    }
                    AppendHeaderArgs(usage, argInfo.Children.ToArray<ArgumentInfo>());
                }
                else
                {
                    if (argInfo.SimpleArgument != null)
                        usage.AppendFormat("[{0}", argInfo.SimpleArgument.HelpText);
                    else
                    {                        
                        if (string.IsNullOrWhiteSpace(argInfo.NamedArgument.ValueHelpText))
                            usage.AppendFormat("[{0}", argInfo.NamedArgument.Names[0]);
                        else
                            usage.AppendFormat("[{0} <{1}>", argInfo.NamedArgument.Names[0], argInfo.NamedArgument.ValueHelpText);
                    }
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
            
            AddArgumentDescriptionLines(lines, this.parser.ArgumentsInfo.Hierarchy);

            return lines.ToArray();
        }

        private void AddArgumentDescriptionLines(List<string> lines, ArgumentInfo[] argsInfo)
        {
            if (argsInfo.Length == 0)
                return;

            int maxArgNamesLength = this.parser.ArgumentsInfo.MaxArgumentsStringLength;
            foreach (ArgumentInfo argInfo in argsInfo)
            {
                if (argInfo.SimpleArgument != null)
                {
                    if (string.IsNullOrWhiteSpace(argInfo.SimpleArgument.Description))
                        continue;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(argInfo.NamedArgument.Description))
                        continue;
                }
                string line = GetHelpLineFromArgument(argInfo, maxArgNamesLength);

                lines.Add(line);
                AddArgumentDescriptionLines(lines, argInfo.Children.ToArray<ArgumentInfo>());
            }
            
        }

        private string GetHelpLineFromArgument(ArgumentInfo argInfo, int maxArgNamesLength)
        {
            StringBuilder builder = new StringBuilder();
            if (argInfo.SimpleArgument != null)
            {
                builder.Append(argInfo.SimpleArgument.HelpText);

                int spaceLeft = maxArgNamesLength - argInfo.SimpleArgument.HelpText.Length;
                for (int i = 0; i < spaceLeft + 1; i++)
                {
                    builder.Append(" ");
                }

                builder.Append(argInfo.SimpleArgument.Description);
            }
            else
            {
                foreach (string name in argInfo.NamedArgument.Names)
                {
                    if (builder.Length > 0)
                        builder.Append(", ");

                    builder.Append(name);
                }

                int spaceLeft = maxArgNamesLength - argInfo.NamedArgument.NamesLength;
                for (int i = 0; i < spaceLeft + 1; i++)
                {
                    builder.Append(" ");
                }

                builder.Append(argInfo.NamedArgument.Description);
            }

            return builder.ToString();
        }
    }
}
