using System;
using System.Runtime.Serialization;

namespace ApiUltraTest.Domain.Enumerable
{
    public enum GenderTypeEnumerable
    {
        [EnumMember(Value = "FEMALE")]
        Female,

        [EnumMember(Value = "MALE")]
        Male
    }
}

