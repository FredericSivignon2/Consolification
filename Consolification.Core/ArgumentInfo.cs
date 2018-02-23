using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class ArgumentInfo
    {
        public ArgumentInfo(CIArgumentAttribute argument)
        {
            if (argument == null)
                throw new ArgumentNullException("argument");

            Argument = argument;
        }

        public CIArgumentAttribute Argument { get; private set; }
        public PropertyInfo PInfo { get; set; }
        public CIMandatoryArgumentAttribute MandatoryArguments { get; set; }
        public CIArgumentBoundaryAttribute ArgumentBoundary { get; set; }
        public CIJobAttribute Job { get; set; }
        public CIChildArgumentAttribute ChildArgument { get; set; }
        public CIParentArgumentAttribute ParentArgument { get; set; }
        public bool Found { get; set; }

        public List<ArgumentInfo> Children { get; } = new List<ArgumentInfo>();

        public override string ToString()
        {
            StringBuilder output = new StringBuilder("Argument: ");
            if (Argument != null)
                output.Append(Argument.Name);
            else
                output.Append("null");
            output.AppendFormat(" - Found: {0}", Found);

            return output.ToString();
        }
    }
}
