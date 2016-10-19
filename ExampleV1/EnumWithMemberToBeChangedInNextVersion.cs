using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public enum EnumWithMemberToBeChangedInNextVersion
    {
        ValueToRemain = 0,
        AnotherValueToRemain = 1,
        ValueToBeChanged = 2,
        [Obsolete("MemberToBeRemoved. Will be removed in version 2.0.0.", true)]
        ObsoleteValueToBeRemoved = 3,
        NonObsoleteValueToBeRemoved = 4
    }
}
