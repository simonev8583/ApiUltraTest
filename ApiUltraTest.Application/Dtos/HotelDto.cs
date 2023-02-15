using ApiUltraTest.Domain.Enumerable;
using ApiUltraTest.Domain.Location;
using ApiUltraTest.Domain;
using System;

namespace ApiUltraTest.Application.Dtos
{
    public class HotelDto
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public Situation? Situation { get; set; }

        public RoomStatusTypeEnumerable? Status { get; set; }

        public List<RoomDto>? Rooms { get; set; }

        public static HotelDto FromDomain(Hotel hotel)
        {
            if (hotel is null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            var dto = new HotelDto();

            dto.Id = hotel.Id;
            dto.Name = hotel.Name;
            dto.Situation = hotel.Situation;
            dto.Status = hotel.Status;

            dto.Rooms = new List<RoomDto>();

            if (hotel.Rooms!.Any())
            {
                hotel.Rooms?.ForEach(room =>
                {
                    dto.Rooms.Add(RoomDto.FromDomain(room));
                });
            }

            return dto;
        }

        public static Hotel ToDomain(HotelDto hotelDto)
        {
            if (hotelDto is null)
            {
                throw new ArgumentNullException(nameof(hotelDto));
            }

            var hotel = new Hotel(hotelDto.Name!, hotelDto.Situation!);

            if (hotelDto?.Status != null)
            {
                hotel.Status = (RoomStatusTypeEnumerable)hotelDto.Status;
            }

            return hotel;
        }
    }
}

