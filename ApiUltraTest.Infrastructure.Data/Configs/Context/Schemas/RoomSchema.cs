using MongoDB.Bson.Serialization.Attributes;
using ApiUltraTest.Domain.Models.RoomModel;
using ApiUltraTest.Domain.Enumerable;
using ApiUltraTest.Domain.Location;
using MongoDB.Bson;
using System;

namespace ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas
{
    public class RoomSchema
    {
        public ObjectId Id { get; set; }

        [BsonElement("roomNumber")]
        public string RoomNumber { get; set; }

        [BsonElement("costBase")]
        public float CostBase { get; set; }

        [BsonElement("tax")]
        public float Tax { get; set; }

        [BsonElement("capacity")]
        public int Capacity { get; set; }

        [BsonElement("roomType")]
        public RoomTypeEnumerable RoomType { get; set; }

        [BsonElement("location")]
        public RoomLocation Location { get; set; }

        [BsonElement("status")]
        public RoomStatusTypeEnumerable Status { get; set; } = RoomStatusTypeEnumerable.Available;

        [BsonElement("hotelId")]
        public string HotelId { get; set; }
    }
}

