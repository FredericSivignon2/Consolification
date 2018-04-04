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
        private class MandatoryGroupInfo
        {
            private List<ArgumentInfo> argumentInfos = new List<ArgumentInfo>();

            public MandatoryGroupInfo(ArgumentInfo argumentInfo)
            {
                this.argumentInfos.Add(argumentInfo);
            }

            public void Add(ArgumentInfo argumentInfo)
            {
                this.argumentInfos.Add(argumentInfo);
            }

            public bool Found { get; set; } = false;
            public ArgumentInfo[] ArgumentInfos
            {
                get
                {
                    return this.argumentInfos.ToArray<ArgumentInfo>();
                }
            }
        }

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
               
            }
        }

        private void ValidateMandatoryArguments(ArgumentInfoCollection argumentsInfo)
        {
            Dictionary<int, MandatoryGroupInfo> groupedMandatories = new Dictionary<int, MandatoryGroupInfo>();
            foreach (ArgumentInfo argumentInfo in argumentsInfo)
            {
                if (argumentInfo.Found == false)
                {
                    ArgumentInfo parentArgInfo = argumentInfo.ParentArgumentInfo;
                    if (parentArgInfo != null)
                    {
                        // If the parent has been found,
                        if (parentArgInfo.Found && argumentInfo.MandatoryArgument != null)
                        {
                          
                                ProcessMandatoryMissing(argumentInfo);
                        }
                        // If the parent has been found,
                        if (parentArgInfo.Found && argumentInfo.GroupedMandatoryArgument != null)
                        {
                                ProcessGroupedMandatories(groupedMandatories, argumentInfo);
                           
                        }
                    }
                    else // The current argument is not a child argument
                    {
                        if (argumentInfo.MandatoryArgument != null)
                        {
                            
                                ProcessMandatoryMissing(argumentInfo);                            
                        }
                        if (argumentInfo.GroupedMandatoryArgument != null)
                        {
                            ProcessGroupedMandatories(groupedMandatories, argumentInfo);
                        }
                    }
                }
                else
                {
                    if (argumentInfo.GroupedMandatoryArgument != null)
                    {
                        if (groupedMandatories.ContainsKey(argumentInfo.GroupedMandatoryArgument.GroupId))
                        {
                            groupedMandatories[argumentInfo.GroupedMandatoryArgument.GroupId].Found = true;
                        }
                        else
                        {
                            MandatoryGroupInfo groupInfo = ProcessGroupedMandatories(groupedMandatories, argumentInfo);
                            groupInfo.Found = true;
                        }
                    }
                }
                ValidateMandatoryArguments(argumentInfo.Children);
            }

            if (groupedMandatories.Count > 0)
            {
                foreach (MandatoryGroupInfo mgInfo in groupedMandatories.Values)
                {
                    if (mgInfo.Found == false)
                    {
                        ProcessGroupedMandatoryMissing(mgInfo.ArgumentInfos);
                    }
                }
            }
        }

        private static MandatoryGroupInfo ProcessGroupedMandatories(Dictionary<int, MandatoryGroupInfo> groupedMandatories, ArgumentInfo argumentInfo)
        {
            MandatoryGroupInfo groupInfo = null;
            int groupId = argumentInfo.GroupedMandatoryArgument.GroupId;
            if (!groupedMandatories.ContainsKey(groupId))
            {
                groupInfo = new MandatoryGroupInfo(argumentInfo);
                groupedMandatories.Add(groupId, groupInfo);
            }
            else
            {
                groupInfo = groupedMandatories[groupId];
                groupInfo.Add(argumentInfo);
            }
            return groupInfo;
        }

        private void ProcessGroupedMandatoryMissing(ArgumentInfo[] argumentInfos)
        {
            // If an argument is missing and if we must not display the command help (in this
            // case, mandatory arguments are not NEEDED!) throw an exception.
            if (parser.MustDisplayHelp == false)
            {
                StringBuilder argList = new StringBuilder();
                foreach (ArgumentInfo argumentInfo in argumentInfos)
                {
                    if (argList.Length > 0)
                        argList.Append(", ");

                    if (argumentInfo.SimpleArgument != null)
                        argList.Append(argumentInfo.SimpleArgument.HelpText);
                    else
                        argList.Append(argumentInfo.NamedArgument.Name);
                }

               throw new GroupedMandatoryArgumentException(argList.ToString());
            }
        }

        private void ProcessMandatoryMissing(ArgumentInfo argumentInfo)
        {
            if (argumentInfo.MandatoryArgument.PromptUser)
            {
                if (!string.IsNullOrEmpty(argumentInfo.MandatoryArgument.PromptMessage))
                {
                    this.parser.Console.Write(argumentInfo.MandatoryArgument.PromptMessage);
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
                            break;

                        case "System.Security.SecureString":
                            SecureString spassword = this.parser.PasswordReader.GetSecurePassword(argumentInfo.Password.PasswordChar);
                            argumentInfo.PInfo.SetValue(this.data, spassword);
                            break;                            

                        default:
                            throw new InvalidArgumentTypeException(string.Format("The type associated with the argument '{0}' is not valid for a password.", argumentInfo.NamedArgument.Name));
                    }
                    this.parser.Console.WriteLine("");
                    return;
                }
                else
                {
                    string line = this.parser.Console.ReadLine();
                    argumentInfo.SetPropertyValue(this.data, line);
                    this.parser.Console.WriteLine("");
                    return;
                }
            }

            // If an argument is missing and if we must not display the command help (in this
            // case, mandatory arguments are not NEEDED!) throw an exception.
            if (parser.MustDisplayHelp == false)
            {
                if (argumentInfo.SimpleArgument != null)
                    throw new MissingMandatoryArgumentException(argumentInfo.SimpleArgument.HelpText);
                else
                    throw new MissingMandatoryArgumentException(argumentInfo.NamedArgument.Name);
            }
        }
        #endregion
    }
}
