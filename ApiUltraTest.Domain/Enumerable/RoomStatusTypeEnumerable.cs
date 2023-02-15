using System;
using System.Runtime.Serialization;

namespace ApiUltraTest.Domain.Enumerable
{
    public enum RoomStatusTypeEnumerable
    {
        [EnumMember(Value = "UNAVAILABLE")]
        Unavailable,

        [EnumMember(Value = "AVAILABLE")]
        Available
    }
}

