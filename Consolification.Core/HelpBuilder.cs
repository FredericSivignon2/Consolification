using System;
using System.Collections.Generic;
using System.Linq;
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

            foreach (ArgumentInfo argInfo in this.argInfos)
            {
                string line = GetHelpLineFromArgument(argInfo);
                lines.Add(line);
            }

            return lines.ToArray();
        }

        private string GetHelpLineFromArgument(ArgumentInfo argInfo)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string name in argInfo.Argument.Names)
            {
                if (builder.Length > 0)
                    builder.Append(", ");

                builder.Append(name);
            }
            builder.Append("\t");
            builder.Append(argInfo.Argument.Description);

            return builder.ToString();
        }
    }
}
