using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    class ArgumentsContainerValidator
    {
        private ArgumentsParser parser;

        public ArgumentsContainerValidator(ArgumentsParser parser)
        {
            this.parser = parser;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Validate()
        {
            ArgumentInfoCollection argumentsInfo = parser.ArgumentsInfo;
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
                            ProcessMandatoryMissing(argumentInfo);
                        }
                    }
                    else // The current argument is not a child argument
                    {
                        if (argumentInfo.Found == false && argumentInfo.MandatoryArguments != null)
                        {
                            ProcessMandatoryMissing(argumentInfo);
                            
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
        
        private void ProcessMandatoryMissing(ArgumentInfo argumentInfo)
        {
            if (argumentInfo.MandatoryArguments.PromptUser)
            {
                if (argumentInfo.MandatoryArguments.Password)
                {
                    // Comment traiter tous les types de manière générique sans tout refaire
                    if (argumentInfo.PInfo.PropertyType.FullName == "System.String")
                    {
                        //this.parser.PasswordReader.GetPassword
                    }

                    
                }
            }

            throw new MissingMandatoryArgumentException(argumentInfo.Argument.Name);
        }
    }
}
