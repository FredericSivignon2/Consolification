using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class ArgumentInfoCollection : List<ArgumentInfo>
    {
        public ArgumentInfo FromName(string name)
        {
            return this.Single<ArgumentInfo>(argInfo => argInfo.Argument.Names.Contains<string>(name));
        }

        /// <summary>
        /// Checks if a mandatory argument is marked as found or not.
        /// </summary>
        /// <returns>The name of the first mandatory element that has not been found.</returns>
        public string MandatoryNotFound()
        {

            ArgumentInfo argInfo = this.FirstOrDefault<ArgumentInfo>(arg => arg.Found == false && arg.MandatoryArguments != null);
            if (argInfo != null)
            {
                return argInfo.Argument.Names[0];
            }
            return null;
        }

        public bool Contains(string[] names)
        {
            foreach (ArgumentInfo argInfo in this)
            {
                // If names defined in the current argument contains at least one element of names
                if (argInfo.Argument.Names.All<string>(str => names.Contains<string>(str) == false) == false)
                    return true;

            }
            return false;
        }
    }
}
