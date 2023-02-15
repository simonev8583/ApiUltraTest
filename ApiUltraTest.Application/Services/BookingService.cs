using ApiUltraTest.Domain.Interfaces.Repository;
using ApiUltraTest.Domain.Models.BookingModel;
using ApiUltraTest.Application.Interfaces;
using ApiUltraTest.Application.Dtos;
using ApiUltraTest.Domain;
using System;

namespace ApiUltraTest.Application.Services
{
    public class BookingService : IBookingService<BookingDto>
    {
        private readonly IBookingRepository<Booking> _bookingProvider;
        private readonly IHotelRepository<Hotel> _hotelProvider;
        private readonly IRoomRepository<Room> _roomProvider;
        private readonly IMailNotificationService _notificationProvider;

        public BookingService(
            IBookingRepository<Booking> bookingRepository,
            IHotelRepository<Hotel> hotelRepository,
            IRoomRepository<Room> roomRepository,
            IMailNotificationService mailNotificationService)
        {
            _bookingProvider = bookingRepository;
            _hotelProvider = hotelRepository;
            _roomProvider = roomRepository;
            _notificationProvider = mailNotificationService;
        }

        public BookingDto Create(BookingDto bookingDto)
        {
            if (bookingDto == null) throw new ArgumentNullException("La información de la reserva es requerida");

            Hotel? hotel = _hotelProvider.GetById(bookingDto.HotelId!);

            if (hotel == null) throw new KeyNotFoundException("No se encontró el hotel con esa referencia");

            Room? room = _roomProvider.GetByIdAndHotel(bookingDto.RoomId!, hotel.Id!);

            if (room == null) throw new KeyNotFoundException("No se encontró la habitación con esa referencia");

            if (bookingDto.Guests!.Count > room.Capacity)
            {
                throw new ArgumentOutOfRangeException("La capacidad de visitantes supera la capacidad de la habitación");
            }

            var bookingStored = _bookingProvider.Create(BookingDto.ToDomain(bookingDto));

            string subject = "Nueva reserva";
            string body = "Con este mail se confirma su nueva reserva de hotel.";
            List<string> mails = new List<string>();

            var mainGuest = bookingStored.Guests.Where(guest => guest.IsOwnBooking == true).FirstOrDefault();

            mails.Add(mainGuest != null ? mainGuest!.Mail : bookingStored.Guests.FirstOrDefault()!.Mail);

            _notificationProvider.SendNotification(subject, body, mails);

            return BookingDto.FromDomain(bookingStored);
        }

        public List<BookingDto> GetByHotelId(string hotelId)
        {
            Hotel? hotel = _hotelProvider.GetById(hotelId);

            if (hotel == null) throw new KeyNotFoundException("No se encontró el hotel con esa referencia");

            List<Booking> bookings = _bookingProvider.GetByHotel(hotelId);

            var result = new List<BookingDto>();

            bookings.ForEach(booking =>
            {
                result.Add(BookingDto.FromDomain(booking));
            });

            return result;

        }

        public BookingDto GetById(string hotelId, string bookinId)
        {
            Hotel? hotel = _hotelProvider.GetById(hotelId);

            if (hotel == null) throw new KeyNotFoundException("No se encontró el hotel con esa referencia");

            Booking? booking = _bookingProvider.GetById(hotelId, bookinId);

            if (booking == null) throw new KeyNotFoundException("No se encontró la reserva");

            return BookingDto.FromDomain(booking);
        }
    }
}

