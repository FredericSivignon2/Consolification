using Consolification.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core
{
    /// <summary>
    /// Represents an ArgumentInfo collection. This special collection handles the argument
    /// hierarchy concept, where we have parents and childrens arguments.
    /// It allows to classify the arguments in a hierachy according to their parent/child attributes.
    /// This is usefull to generate the corresponding command help text!
    /// </summary>
    public class ArgumentInfoCollection : IEnumerable<ArgumentInfo>
    {
        #region Data Members
        private List<ArgumentInfo> argumentInfos = new List<ArgumentInfo>();
        // List of top parent items
        private List<ArgumentInfo> hierarchy;
        #endregion

        #region Public Properties
        public int Count
        {
            get
            {
                return this.argumentInfos.Count;
            }
        }
        public ArgumentInfo this[int index]
        {
            get
            {
                return this.argumentInfos[index];
            }
        }
        /// <summary>
        /// Gets an array of ArgumentInfo for which all elements are top parent argument
        /// (meaning that those arguments are not children arguments).
        /// </summary>
        public ArgumentInfo[] Hierarchy
        {
            get
            {
                if (hierarchy == null)
                    ComputeHierarchy();
                return hierarchy.ToArray<ArgumentInfo>();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a new ArgumentInfo instance to this collection.
        /// </summary>
        /// <param name="argInfo"></param>
        public void Add(ArgumentInfo argInfo)
        {
            this.argumentInfos.Add(argInfo);
        }

        public void AddRange(ArgumentInfoCollection items)
        {
            this.argumentInfos.AddRange(items.argumentInfos);
        }

        /// <summary>
        /// Gets a boolean value that indicates whether this instance contains an argument
        /// for which the name is equal to the specified string.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Contains(string name)
        {
            return this.argumentInfos.Exists(argInfo =>
            {
                if (argInfo.NamedArgument == null)
                    return false;

                return argInfo.NamedArgument.Names.Contains<string>(name);
            });
        }

        public bool DeepContains(string name)
        {
            return DeepContains(name, this.argumentInfos);
        }

        private bool DeepContains(string name, List<ArgumentInfo> args)
        {
            return args.Exists(argInfo =>
            {
                if (argInfo.NamedArgument != null)
                {
                    if (argInfo.NamedArgument.Names.Contains<string>(name))
                        return true;
                }
                if (argInfo.Children.Count > 0)
                    return DeepContains(name, argInfo.Children.argumentInfos);
                else
                    return false;
            });
        }
        /// <summary>
        /// Gets a boolean value that indicates whether this instance contains an argument
        /// for which the name is equal to the specified string. Also look into
        /// children.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArgumentInfo GetParentArgument(string name)
        {
            return GetParentArgument(name, this.argumentInfos);   
        }

        private static ArgumentInfo GetParentArgument(string name, List<ArgumentInfo> items)
        {
            foreach (ArgumentInfo parentArgInfo in items)
            {
                if (parentArgInfo.Children.Contains(name))
                    return parentArgInfo;

                ArgumentInfo foundArgInfo = GetParentArgument(name, parentArgInfo.Children.argumentInfos);
                if (foundArgInfo != null)
                    return foundArgInfo;
            }
            return null;
        }

        /// <summary>
        /// Gets a boolean value that indicates whether this instance represents an argument
        /// for which names contains at least one element of the given string array.
        /// </summary>
        /// <param name="names"></param>
        /// <returns>true if we found at least one string from the given array that is equals to
        /// the name(s) of this argument.</returns>
        public bool Contains(string[] names)
        {
            foreach (ArgumentInfo argInfo in this.argumentInfos)
            {
                if (argInfo.NamedArgument == null)
                    continue;

                if (argInfo.NamedArgument.Names.All<string>(str => names.Contains<string>(str) == false) == false)
                    return true;

            }
            return false;
        }

        public ArgumentInfo GetSimpleArgument(int positionIndex)
        {
            return this.argumentInfos.Find(argInfo =>
            {
                if (argInfo.SimpleArgument != null)
                    return argInfo.SimpleArgumentIndex == positionIndex;

                return false;
            });
        }

        /// <summary>
        /// Gets the ArgumentInfo instance corresponding to the given name.
        /// </summary>
        /// <param name="name">An argument name (could be the regular name or a shortcut name).</param>
        /// <returns></returns>
        public ArgumentInfo FromName(string name)
        {
            return this.argumentInfos.Single<ArgumentInfo>(argInfo =>
            {
                if (argInfo.SimpleArgument != null)
                    return false;

                return argInfo.NamedArgument.Names.Contains<string>(name);
            });
        }

        /// <summary>
        /// Gets the parent ArgumentInfo if any.
        /// </summary>
        /// <param name="parentId">The identifier of the parent argument.</param>
        /// <returns>An ArgumentInfo that reprensents the parent argument if any. Otheriwe, returns null.</returns>
        public ArgumentInfo GetParent(int parentId)
        {
            return GetParent(this.argumentInfos, parentId);
        }
        #endregion

        #region Overridden
        public override string ToString()
        {
            return string.Format("Count = {0} (Hierarchy count: {1}", this.argumentInfos.Count,
                this.hierarchy == null ? "N/A" : this.hierarchy.Count.ToString());
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
            ArgumentInfo argInfo = argumentInfos.Find((argumentInfo) =>
                   {
                       return argumentInfo.ParentArgument != null && argumentInfo.ParentArgument.Id == parentId;
                   });

            if (argInfo != null)
                return argInfo;

            foreach (ArgumentInfo curArgInfo in argumentInfos)
            {
                argInfo = GetParent(curArgInfo.Children.argumentInfos, parentId);
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
