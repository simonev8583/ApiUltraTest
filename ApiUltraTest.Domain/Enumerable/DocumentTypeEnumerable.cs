using System;
using System.Runtime.Serialization;

namespace ApiUltraTest.Domain.Enumerable
{
    public enum DocumentTypeEnumerable
    {
        [EnumMember(Value = "ID")]
        Id,

        [EnumMember(Value = "PASSPORT")]
        Passport
    }
}

