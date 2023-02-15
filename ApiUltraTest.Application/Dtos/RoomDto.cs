using ApiUltraTest.Domain.Models.RoomModel;
using ApiUltraTest.Domain.Enumerable;
using ApiUltraTest.Domain;
using System;

namespace ApiUltraTest.Application.Dtos
{
    public class RoomDto
    {
        public string? Id { get; set; }

        public string? RoomNumber { get; set; }

        public float? CostBase { get; set; }

        public float? Tax { get; set; }

        public int Capacity { get; set; }

        public RoomTypeEnumerable? RoomType { get; set; }

        public RoomLocation? Location { get; set; }

        public RoomStatusTypeEnumerable? Status { get; set; } = RoomStatusTypeEnumerable.Available;

        public string? HotelId { get; set; }

        public static RoomDto FromDomain(Room room)
        {
            if (room is null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            RoomDto dto = new RoomDto();

            dto.RoomNumber = room.RoomNumber;
            dto.CostBase = room.CostBase;
            dto.Tax = room.Tax;
            dto.RoomType = room.RoomType;
            dto.Location = room.Location;
            dto.Status = room.Status;
            dto.Id = room.Id;
            dto.Capacity = room.Capacity;

            dto.HotelId = room.HotelId;


            return dto;
        }
    }
}

