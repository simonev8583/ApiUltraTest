using ApiUltraTest.Domain.Enumerable;
using ApiUltraTest.Domain.Location;
using System;

namespace ApiUltraTest.Domain
{
    public class Hotel
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public Situation Situation { get; set; }

        public List<Room>? Rooms { get; set; }

        public RoomStatusTypeEnumerable Status { get; set; } = RoomStatusTypeEnumerable.Available;

        public Hotel(string name, Situation situation)
        {
            Name = name;
            Situation = situation;

            Rooms = new List<Room>();
        }
    }
}

