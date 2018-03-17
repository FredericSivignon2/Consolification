using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public static class TypeExtension
    {
        private const string SystemName = "System";
        private const string CommonLRLib = "CommonLanguageRuntimeLibrary";

        public static bool IsBuildinType(this Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }

        public static bool IsUserType(this Type type)
        {
            if (type.Namespace == SystemName ||
                type.Namespace.StartsWith(SystemName) ||
                type.Module.ScopeName == CommonLRLib ||
                IsBuildinType(type))
                return false;

            return true;
        }
    }
}
