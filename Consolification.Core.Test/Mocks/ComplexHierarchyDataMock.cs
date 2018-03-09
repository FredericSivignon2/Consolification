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
    public class ComplexHierarchyDataMock
    {
        [CINamedArgument("/ARG1", "This is the ARG1 argument.", "value")]
        public bool Arg1 { get; private set; }

        [CINamedArgument("/CHILDTOPA1", "This is the CHILDTOPA1 argument.", "value")]
        [CIChildArgument(1)]
        public string ChildTopAArg1 { get; private set; }

        [CINamedArgument("/CHILDMID1", "This is the CHILDMID1 argument.", "value")]
        [CIChildArgument(2)]
        [CIMandatoryArgument()]
        public string ChildMidArg1 { get; private set; }

        [CINamedArgument("/CHILDMID2", "This is the CHILDMID2 argument.", "value")]
        [CIChildArgument(2)]
        public string ChildMidArg2 { get; private set; }

        [CINamedArgument("/CHILDBACK2", "This is the CHILDBACK2 argument.", "value")]
        [CIChildArgument(3)]
        public string ChildBackArg2 { get; private set; }

        [CINamedArgument("/MIDB", "This is the MIDB argument.", "value")]
        [CIChildArgument(4)]
        [CIParentArgument(5)]
        public string MidBArg { get; private set; }

        [CINamedArgument("/CHILDMIDB2", "This is the CHILDMIDB2 argument.", "value")]
        [CIChildArgument(5)]
        public string ChildMidBArg2 { get; private set; }

        [CINamedArgument("/CHILDTOPB1", "This is the CHILDTOPB1 argument.", "value")]
        [CIChildArgument(4)]
        public string ChildTopBArg1 { get; private set; }

        [CINamedArgument("/MID", "This is the MID argument.", "value")]
        [CIParentArgument(2)]
        [CIChildArgument(1)]
        public string MidArg { get; private set; }

        [CINamedArgument("/TOPA", "This is the TOPA argument.", "value")]
        [CIParentArgument(1)]
        public string TopAArg { get; private set; }

        [CINamedArgument("/BACK", "This is the BACK argument.", "value")]
        [CIChildArgument(2)]
        [CIParentArgument(3)]
        [CIMandatoryArgument()]
        public string BackArg { get; private set; }

        [CINamedArgument("/CHILDBACK1", "This is the CHILDBACK1 argument.", "value")]
        [CIChildArgument(3)]
        public string ChildBackArg1 { get; private set; }

        [CINamedArgument("/TOPB", "This is the TOPB argument.", "value")]
        [CIParentArgument(4)]
        public string ToBAArg { get; private set; }

        [CINamedArgument("/ARG2", "This is the ARG2 argument.", "value")]
        public bool Arg2 { get; private set; }

        [CINamedArgument("/CHILDTOPB2", "This is the CHILDTOPB2 argument.", "value")]
        [CIChildArgument(4)]
        public string ChildTopBArg2 { get; private set; }

        [CINamedArgument("/CHILDMIDB1", "This is the CHILDMIDB1 argument.", "value")]
        [CIChildArgument(5)]
        public string ChildMidBArg1 { get; private set; }
    }
}
