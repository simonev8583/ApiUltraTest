using ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas.SubSchemas;
using MongoDB.Bson.Serialization.Attributes;
using ApiUltraTest.Domain.Enumerable;
using ApiUltraTest.Domain.Location;
using MongoDB.Bson;
using System;

namespace ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas
{
    public class BookingSchema
    {
        public ObjectId Id { get; set; }

        [BsonElement("hotelId")]
        public string HotelId { get; set; }

        [BsonElement("roomId")]
        public string RoomId { get; set; }

        [BsonElement("guests")]
        public List<GuestSubSchema> Guests { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

    }
}

