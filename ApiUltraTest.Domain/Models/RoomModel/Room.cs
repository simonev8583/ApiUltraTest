using ApiUltraTest.Domain.Models.RoomModel;
using ApiUltraTest.Domain.Enumerable;
using System;

namespace ApiUltraTest.Domain
{
    public class Room
    {
        public string? Id { get; set; }

        public string RoomNumber { get; set; }

        public float CostBase { get; set; }

        public float Tax { get; set; }

        public int Capacity { get; set; }

        public RoomTypeEnumerable RoomType { get; set; }

        public RoomLocation Location { get; set; }

        public RoomStatusTypeEnumerable Status { get; set; } = RoomStatusTypeEnumerable.Available;

        public string HotelId { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;

        public Room(string roomNumber, RoomLocation location, string hotelId)
        {
            RoomNumber = roomNumber;
            HotelId = hotelId;
            Location = location;
        }

    }
}

