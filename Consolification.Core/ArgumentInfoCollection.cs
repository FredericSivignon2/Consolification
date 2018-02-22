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
            return this.Single<ArgumentInfo>(argInfo => 
                argInfo.Argument.Names.Contains<string>(name));
        }

        /// <summary>
        /// Gets the maximum length of argument names (the length of the string composed of all name associated with one argument
        /// where each argument is separated by a comman then a space) found in this collection.
        /// </summary>
        public int MaxArgumentsStringLength
        {
            get
            {
                int max = 0;
                foreach (ArgumentInfo argInfo in this)
                {
                    if (argInfo.Argument.NamesLength > max)
                        max = argInfo.Argument.NamesLength;
                }
                return max;
            }
        }


        /// <summary>
        /// Checks if a mandatory argument is marked as found or not.
        /// </summary>
        /// <returns>The name of the first mandatory element that has not been found.</returns>
        /*public string MandatoryNotFound()
        {

            ArgumentInfo argInfo = this.FirstOrDefault<ArgumentInfo>(arg => arg.Found == false && arg.MandatoryArguments != null);
            if (argInfo != null)
            {
                return argInfo.Argument.Names[0];
            }
            return null;
        }*/

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

        public ArgumentInfo GetParent(int parentId)
        {
            return Find((argumentInfo) =>
            {
                return argumentInfo.ParentArgument != null && argumentInfo.ParentArgument.Id == parentId;
            });
        }
    }
}
