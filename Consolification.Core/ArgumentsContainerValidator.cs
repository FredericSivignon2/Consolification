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

        /// <summary>
        /// 
        /// </summary>
        public void Validate()
        {
            ArgumentInfoCollection argumentsInfo = container.ArgumentsInfo;
            ValidateParentChildrenArguments(argumentsInfo);
            ValidateMandatoryArguments(argumentsInfo);
        }

        private void ValidateParentChildrenArguments(ArgumentInfoCollection argumentsInfo)
        {
            foreach (ArgumentInfo argumentInfo in argumentsInfo)
            {
                ArgumentInfo curArgInfo = argumentInfo;
                if (curArgInfo.Found == true)
                {
                    // While the current argument is a child argument
                    while (curArgInfo.ChildArgument != null)
                    {
                        ArgumentInfo parentArgInfo = GetParent(argumentsInfo, curArgInfo);
                        if (parentArgInfo.Found == false)
                            throw new MissingParentArgumentException(parentArgInfo.Argument.Name);

                        curArgInfo = parentArgInfo;
                    }
                }
            }
        }

        private void ValidateMandatoryArguments(ArgumentInfoCollection argumentsInfo)
        {
            foreach (ArgumentInfo argumentInfo in argumentsInfo)
            {
                if (argumentInfo.Found == false)
                {
                    if (argumentInfo.ChildArgument != null)
                    {
                        ArgumentInfo parentArgInfo = GetParent(argumentsInfo, argumentInfo);
                        // If the parent has been found,
                        if (parentArgInfo.Found && argumentInfo.MandatoryArguments != null)
                        {
                            throw new MissingMandatoryArgumentException(argumentInfo.Argument.Name);
                        }
                    }
                    else // The current argument is not a child argument
                    {
                        if (argumentInfo.Found == false && argumentInfo.MandatoryArguments != null)
                        {
                            throw new MissingMandatoryArgumentException(argumentInfo.Argument.Name);
                        }
                    }
                }
            }
        }

        private ArgumentInfo GetParent(ArgumentInfoCollection argumentsInfo, ArgumentInfo argumentInfo)
        {
            if (argumentInfo.ChildArgument == null)
                throw new InvalidOperationException("argumentInfo.ChildArgument could not be null for this method!");

            ArgumentInfo parentArgInfo = argumentsInfo.GetParent(argumentInfo.ChildArgument.ParentId);
            if (parentArgInfo == null)
            {
                throw new UnknownParentArgumentAttributeException(argumentInfo.ChildArgument.ParentId, argumentInfo.PInfo.Name);
            }
            return parentArgInfo;
        }        
    }
}
