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
        public AppArgumentAttribute Argument { get; set; }
        public AppMandatoryArgumentAttribute MandatoryArguments { get; set; }
        public bool Found { get; set; }
    }
}
