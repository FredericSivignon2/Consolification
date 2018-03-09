using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    /// <summary>
    /// Valides data within an ArgumentsParser instance to ensure that given command arguments
    /// are valid regarding corresponding data object definition.
    /// </summary>
    class ArgumentsParserValidator
    {
        #region Data Members
        private ArgumentsParser parser;
        private object data;
        #endregion

        #region Constructors
        public ArgumentsParserValidator(ArgumentsParser parser, object data)
        {
            this.parser = parser;
            this.data = data;
        }
        #endregion

        #region Public Methods
        public void Validate()
        {
            ArgumentInfoCollection argumentsInfo = parser.ArgumentsInfo;
            ValidateParentChildrenArguments(argumentsInfo);
            ValidateMandatoryArguments(argumentsInfo);
        }
        #endregion

        #region Private Methods
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
                            throw new MissingParentArgumentException(parentArgInfo.NamedArgument.Name);

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
                if (!string.IsNullOrEmpty(argumentInfo.MandatoryArguments.PromptMessage))
                {
                    this.parser.Console.Write(argumentInfo.MandatoryArguments.PromptMessage);
                }
                else
                {
                    this.parser.Console.Write("? ");
                }

                if (argumentInfo.Password != null)
                {
                    switch (argumentInfo.PInfo.PropertyType.FullName)
                    {
                        case "System.String":
                            string password = this.parser.PasswordReader.GetPassword(argumentInfo.Password.PasswordChar);
                            argumentInfo.PInfo.SetValue(this.data, password);
                            this.parser.Console.WriteLine("");
                            return;

                        case "System.Security.SecureString":
                            SecureString spassword = this.parser.PasswordReader.GetSecurePassword(argumentInfo.Password.PasswordChar);
                            argumentInfo.PInfo.SetValue(this.data, spassword);
                            this.parser.Console.WriteLine("");
                            return;

                        default:
                            throw new InvalidArgumentTypeException(string.Format("The type associated with the argument '{0}' is not valid for a password.", argumentInfo.NamedArgument.Name));
                    }                    
                }
                else
                {
                    string line = this.parser.Console.ReadLine();
                    argumentInfo.SetPropertyValue(this.data, line);
                    this.parser.Console.WriteLine("");
                    return;
                }
            }

            if (argumentInfo.SimpleArgument != null)
                throw new MissingMandatoryArgumentException(argumentInfo.SimpleArgument.HelpText);
            else
                throw new MissingMandatoryArgumentException(argumentInfo.NamedArgument.Name);
        }
        #endregion
    }
}
