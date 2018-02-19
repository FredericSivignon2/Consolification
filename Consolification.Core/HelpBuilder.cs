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
        private ArgumentInfoCollection argInfos;

        public HelpBuilder(ArgumentInfoCollection argInfos)
        {
            this.argInfos = argInfos;
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
            
            StringBuilder usage = new StringBuilder("Usage:");
            usage.Append(GetExeName());

            foreach (ArgumentInfo argInfo in this.argInfos)
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
            lines.Add(usage.ToString());

            return lines.ToArray();
        }

        private string GetExeName()
        {
            return Assembly.GetEntryAssembly().GetName().Name;
        }

        private string[] GetArgumentsDescriptionLines()
        {
            List<string> lines = new List<string>();
            int maxArgNamesLength = this.argInfos.MaxArgumentsStringLength;
            foreach (ArgumentInfo argInfo in this.argInfos)
            {
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
