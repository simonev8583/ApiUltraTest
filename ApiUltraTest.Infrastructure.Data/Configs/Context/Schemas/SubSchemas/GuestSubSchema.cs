using System;
using ApiUltraTest.Domain.Enumerable;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas.SubSchemas
{
    public class GuestSubSchema
    {
        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("birthDate")]
        public DateTime BirthDate { get; set; }

        [BsonElement("genderType")]
        public GenderTypeEnumerable GenderType { get; set; }

        [BsonElement("documentType")]
        public DocumentTypeEnumerable DocumentType { get; set; }

        [BsonElement("documentNumber")]
        public string DocumentNumber { get; set; }

        [BsonElement("mail")]
        public string Mail { get; set; }

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("isOwnBooking")]
        public bool IsOwnBooking { get; set; }
    }
}

