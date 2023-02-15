using MongoDB.Bson.Serialization.Attributes;
using ApiUltraTest.Domain.Enumerable;
using ApiUltraTest.Domain.Location;
using MongoDB.Bson;
using System;

namespace ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas
{
    public class HotelSchema
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("situation")]
        public Situation Situation { get; set; }

        [BsonElement("status")]
        public RoomStatusTypeEnumerable Status { get; set; } = RoomStatusTypeEnumerable.Available;
    }
}

