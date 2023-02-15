using System;
using ApiUltraTest.Domain.Models.GuestModel;

namespace ApiUltraTest.Domain.Models.BookingModel
{
    public class Booking
    {
        public string? Id { get; set; }

        public string HotelId { get; set; }

        public string RoomId { get; set; }

        public List<Guest> Guests { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Booking(string hotelId, string roomId, List<Guest> guests, DateTime startDate, DateTime endDate)
        {
            HotelId = hotelId;
            RoomId = roomId;
            Guests = guests;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}

