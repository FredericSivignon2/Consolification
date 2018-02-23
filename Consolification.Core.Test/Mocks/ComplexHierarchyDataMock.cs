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
    [CIHelpArgument("/?")]
    public class ComplexHierarchyDataMock : ArgumentsContainer
    {
        [CIArgument("/ARG1", "This is the ARG1 argument.")]
        public bool Arg1 { get; private set; }

        [CIArgument("/CHILDTOPA1", "This is the CHILDTOPA1 argument.")]
        [CIChildArgument(1)]
        public string ChildTopAArg1 { get; private set; }

        [CIArgument("/CHILDMID1", "This is the CHILDMID1 argument.")]
        [CIChildArgument(2)]
        [CIMandatoryArgument()]
        public string ChildMidArg1 { get; private set; }

        [CIArgument("/CHILDMID2", "This is the CHILDMID2 argument.")]
        [CIChildArgument(2)]
        public string ChildMidArg2 { get; private set; }

        [CIArgument("/CHILDBACK2", "This is the CHILDBACK2 argument.")]
        [CIChildArgument(3)]
        public string ChildBackArg2 { get; private set; }

        [CIArgument("/MIDB", "This is the MIDB argument.")]
        [CIChildArgument(4)]
        [CIParentArgument(5)]
        public string MidBArg { get; private set; }

        [CIArgument("/CHILDMIDB2", "This is the CHILDMIDB2 argument.")]
        [CIChildArgument(5)]
        public string ChildMidBArg2 { get; private set; }

        [CIArgument("/CHILDTOPB1", "This is the CHILDTOPB1 argument.")]
        [CIChildArgument(4)]
        public string ChildTopBArg1 { get; private set; }

        [CIArgument("/MID", "This is the MID argument.")]
        [CIParentArgument(2)]
        [CIChildArgument(1)]
        public string MidArg { get; private set; }

        [CIArgument("/TOPA", "This is the TOPA argument.")]
        [CIParentArgument(1)]
        public string TopAArg { get; private set; }

        [CIArgument("/BACK", "This is the BACK argument.")]
        [CIChildArgument(2)]
        [CIParentArgument(3)]
        [CIMandatoryArgument()]
        public string BackArg { get; private set; }

        [CIArgument("/CHILDBACK1", "This is the CHILDBACK1 argument.")]
        [CIChildArgument(3)]
        public string ChildBackArg1 { get; private set; }

        [CIArgument("/TOPB", "This is the TOPB argument.")]
        [CIParentArgument(4)]
        public string ToBAArg { get; private set; }

        [CIArgument("/ARG2", "This is the ARG2 argument.")]
        public bool Arg2 { get; private set; }

        [CIArgument("/CHILDTOPB2", "This is the CHILDTOPB2 argument.")]
        [CIChildArgument(4)]
        public string ChildTopBArg2 { get; private set; }

        [CIArgument("/CHILDMIDB1", "This is the CHILDMIDB1 argument.")]
        [CIChildArgument(5)]
        public string ChildMidBArg1 { get; private set; }
    }
}
