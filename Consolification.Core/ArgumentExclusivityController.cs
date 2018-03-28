using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    class ArgumentExclusivityController
    {
        Dictionary<int, List<ArgumentInfo>> foundArguments = new Dictionary<int, List<ArgumentInfo>>();

        public ArgumentExclusivityController()
        {
        }

        public void NewArgument(ArgumentInfo argumentInfo)
        {
            int groupId = 0;
            if (argumentInfo.Exclusive != null)
            {
                ArgumentInfo existingArgInfo = ArgumentExists(argumentInfo.Exclusive.GroupId);

                if (existingArgInfo != null)
                    throw new ExclusiveArgumentException(argumentInfo, existingArgInfo);

                groupId = argumentInfo.Exclusive.GroupId;
            }
            else
            {
                ArgumentInfo existingArgInfo = ExclusiveArgumentExistsAtLevel0();
                if (existingArgInfo != null)
                    throw new ExclusiveArgumentException(existingArgInfo, argumentInfo);
            }

            List<ArgumentInfo> list = null;
            if (foundArguments.ContainsKey(groupId))
            {
                list = foundArguments[groupId];                
            }
            else
            {
                list = new List<ArgumentInfo>();
                foundArguments.Add(groupId, list);
            }
            list.Add(argumentInfo);
        }

        private ArgumentInfo ArgumentExists(int groupId)
        {
            if (foundArguments.ContainsKey(groupId))
            {
                return foundArguments[groupId][0];
            }
            return null;
        }

        private ArgumentInfo ExclusiveArgumentExistsAtLevel0()
        {
            if (foundArguments.ContainsKey(0) == false)
                return null;

            return foundArguments[0].FirstOrDefault<ArgumentInfo>(argInfo =>
            {
                return (argInfo.Exclusive != null);                    
            });
        }
    }
}
