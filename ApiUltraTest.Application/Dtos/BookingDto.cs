using ApiUltraTest.Domain;
using ApiUltraTest.Domain.Models.BookingModel;
using ApiUltraTest.Domain.Models.GuestModel;
using System;

namespace ApiUltraTest.Application.Dtos
{
    public class BookingDto
    {
        public string? Id { get; set; }

        public string? HotelId { get; set; }

        public string? RoomId { get; set; }

        public List<Guest>? Guests { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public static BookingDto FromDomain(Booking booking)
        {
            var dto = new BookingDto();

            dto.Id = booking.Id;
            dto.HotelId = booking.HotelId;
            dto.RoomId = booking.RoomId;
            dto.Guests = booking.Guests;
            dto.StartDate = booking.StartDate;
            dto.EndDate = booking.EndDate;

            return dto;
        }

        public static Booking ToDomain(BookingDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            Booking booking = new Booking(dto.HotelId!, dto.RoomId!, dto.Guests!, dto.StartDate, dto.EndDate);

            if (dto.Id != null)
            {
                booking.Id = dto.Id;
            }

            return booking;
        }
    }
}

