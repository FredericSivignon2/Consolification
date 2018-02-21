using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class ArgumentInfo
    {
        public PropertyInfo PInfo { get; set; }
        public CIArgumentAttribute Argument { get; set; }
        public CIMandatoryArgumentAttribute MandatoryArguments { get; set; }
        public CIArgumentBoundaryAttribute ArgumentBoundary { get; set; }
        public CIJobAttribute Job { get; set; }
        public CIChildArgumentAttribute ChildArgument { get; set; }
        public CIParentArgumentAttribute ParentArgument { get; set; }
        public bool Found { get; set; }
    }
}
