using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    public class ArgumentInfoCollection : IEnumerable<ArgumentInfo>
    {
        #region Data Members
        private List<ArgumentInfo> argumentInfos = new List<ArgumentInfo>();
        // List of top parent items
        private List<ArgumentInfo> hierarchy;
        #endregion

        #region Public Properties
        public ArgumentInfo FromName(string name)
        {
            return this.argumentInfos.Single<ArgumentInfo>(argInfo => 
                argInfo.Argument.Names.Contains<string>(name));
        }

        public ArgumentInfo[] Hierarchy
        {
            get
            {
                if (hierarchy == null)
                    ComputeHierarchy();
                return hierarchy.ToArray<ArgumentInfo>();
            }
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
                foreach (ArgumentInfo argInfo in this.argumentInfos)
                {
                    if (argInfo.Argument.NamesLength > max)
                        max = argInfo.Argument.NamesLength;
                }
                return max;
            }
        }
        #endregion

        #region Public Methods
        public void Add(ArgumentInfo argInfo)
        {
            this.argumentInfos.Add(argInfo);
        }

        public bool Contains(string[] names)
        {
            foreach (ArgumentInfo argInfo in this.argumentInfos)
            {
                // If names defined in the current argument contains at least one element of names
                if (argInfo.Argument.Names.All<string>(str => names.Contains<string>(str) == false) == false)
                    return true;

            }
            return false;
        }

        public ArgumentInfo GetParent(int parentId)
        {
            return GetParent(this.argumentInfos, parentId);
        }
        #endregion

        #region  IEnumerable<ArgumentInfo> implementation
        public IEnumerator<ArgumentInfo> GetEnumerator()
        {
            return this.argumentInfos.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.argumentInfos.GetEnumerator();
        }
        #endregion

        #region Private Methods
        private ArgumentInfo GetParent(List<ArgumentInfo> argumentInfos, int parentId)
        {
            ArgumentInfo argInfo =  argumentInfos.Find((argumentInfo) =>
                    {
                        return argumentInfo.ParentArgument != null && argumentInfo.ParentArgument.Id == parentId;
                    });

            if (argInfo != null)
                return argInfo;

            foreach (ArgumentInfo curArgInfo in argumentInfos)
            {
                argInfo = GetParent(curArgInfo.Children, parentId);
                if (argInfo != null)
                    return argInfo;
            }
            return null;
        }

        private void ComputeHierarchy()
        {
            hierarchy = new List<ArgumentInfo>();

            Dictionary<int, List<ArgumentInfo>> tempParentInfo = new Dictionary<int, List<ArgumentInfo>>();
            foreach (ArgumentInfo argInfo in this.argumentInfos)
            {
                // If the current argument is not a parent and not a child, put it on top level
                if (argInfo.ParentArgument == null && argInfo.ChildArgument == null)
                {
                    this.hierarchy.Add(argInfo);
                    continue;
                }

                ArgumentInfo argInfoParent = null;
                if (argInfo.ChildArgument != null)
                {
                    argInfoParent = GetParent(argInfo.ChildArgument.ParentId);
                    argInfoParent.Children.Add(argInfo);
                }
                else
                if (argInfo.ParentArgument != null)
                {
                    this.hierarchy.Add(argInfo);
                }
            }
        }
        #endregion
    }
}
