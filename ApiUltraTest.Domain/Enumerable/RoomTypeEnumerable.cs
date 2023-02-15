using System;
using System.Runtime.Serialization;

namespace ApiUltraTest.Domain.Enumerable
{
    public enum RoomTypeEnumerable
    {
        [EnumMember(Value = "SIMPLE")]
        Simple,

        [EnumMember(Value = "DOUBLE")]
        Double,

        [EnumMember(Value = "SUITE")]
        Suite,

    }
}

