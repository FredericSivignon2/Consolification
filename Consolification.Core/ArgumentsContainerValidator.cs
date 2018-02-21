using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class ArgumentsContainerValidator
    {
        private ArgumentsContainer container;

        public ArgumentsContainerValidator(ArgumentsContainer container)
        {
            this.container = container;
        }

        public void Validate()
        {
            ArgumentInfoCollection argumentsInfo = container.ArgumentsInfo;

            foreach (ArgumentInfo argumentInfo in argumentsInfo)
            {
                if (argumentInfo.Argument == null)
                    continue;

                if (argumentInfo.Found == false)
                {
                    if (argumentInfo.ChildArgument != null)
                    {
                        ArgumentInfo parentArgInfo = argumentsInfo.GetParent(argumentInfo.ChildArgument.ParentId);
                        if (parentArgInfo == null)
                        {
                            throw new MissingParentArgumentAttributeException(string.Format("The parent argument identifier '{0}' specified for the property '{1}' does not exist.", 
                                argumentInfo.ChildArgument.ParentId, argumentInfo.PInfo.Name));
                        }
                    }
                }
            }
            string argNotFound = argumentsInfo.MandatoryNotFound();
            if (argNotFound != null)
            {
                throw new MissingArgumentException(argNotFound);
            }

        }
    }
}
