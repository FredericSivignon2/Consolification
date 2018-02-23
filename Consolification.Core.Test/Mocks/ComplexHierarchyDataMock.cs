using Consolification.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolification.Core.Test.Mocks
{
    /// <summary>
    /// Hierarchy describes here:
    /// 
    /// ARG1
    /// TOPA (1)
    ///     CHILDTOPA1
    ///     MID (2)
    ///         CHILDMID1
    ///         CHILDMID2
    ///         BACK (3)
    ///             CHILDBACK2
    ///             CHILDBACK1
    /// TOPB (4)
    ///     MIDB (5)
    ///         CHILDMIDB2
    ///         CHILDMIDB1
    ///     CHILDTOPB1
    ///     CHILDTOPB2
    /// ARG2
    /// </summary>
    public class ComplexHierarchyDataMock : ArgumentsContainer
    {
        [CIArgument("/ARG1")]
        public bool Arg1 { get; private set; }

        [CIArgument("/CHILDTOPA1")]
        [CIChildArgument(1)]
        public string ChildTopAArg1 { get; private set; }

        [CIArgument("/CHILDMID1")]
        [CIChildArgument(2)]
        [CIMandatoryArgument()]
        public string ChildMidArg1 { get; private set; }

        [CIArgument("/CHILDMID2")]
        [CIChildArgument(2)]
        public string ChildMidArg2 { get; private set; }

        [CIArgument("/CHILDBACK2")]
        [CIChildArgument(3)]
        public string ChildBackArg2 { get; private set; }

        [CIArgument("/MIDB")]
        [CIChildArgument(4)]
        [CIParentArgument(5)]
        public string MidBArg { get; private set; }

        [CIArgument("/CHILDMIDB2")]
        [CIChildArgument(5)]
        public string ChildMidBArg2 { get; private set; }

        [CIArgument("/CHILDTOPB1")]
        [CIChildArgument(4)]
        public string ChildTopBArg1 { get; private set; }

        [CIArgument("/MID")]
        [CIParentArgument(2)]
        [CIChildArgument(1)]
        public string MidArg { get; private set; }

        [CIArgument("/TOPA")]
        [CIParentArgument(1)]
        public string TopAArg { get; private set; }

        [CIArgument("/BACK")]
        [CIChildArgument(2)]
        [CIParentArgument(3)]
        [CIMandatoryArgument()]
        public string BackArg { get; private set; }

        [CIArgument("/CHILDBACK1")]
        [CIChildArgument(3)]
        public string ChildBackArg1 { get; private set; }

        [CIArgument("/TOPB")]
        [CIParentArgument(4)]
        public string ToBAArg { get; private set; }

        [CIArgument("/ARG2")]
        public bool Arg2 { get; private set; }

        [CIArgument("/CHILDTOPB2")]
        [CIChildArgument(4)]
        public string ChildTopBArg2 { get; private set; }

        [CIArgument("/CHILDMIDB1")]
        [CIChildArgument(5)]
        public string ChildMidBArg1 { get; private set; }
    }
}
