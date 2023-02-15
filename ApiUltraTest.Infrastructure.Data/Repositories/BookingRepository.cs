using ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas.SubSchemas;
using ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas;
using ApiUltraTest.Infrastructure.Data.Configs.Context;
using ApiUltraTest.Domain.Interfaces.Repository;
using ApiUltraTest.Domain.Models.BookingModel;
using ApiUltraTest.Domain.Models.GuestModel;
using ApiUltraTest.Domain;
using MongoDB.Bson;
using System;
using ApiUltraTest.Domain.Models;
using MongoDB.Driver;

namespace ApiUltraTest.Infrastructure.Data.Repositories
{
    public class BookingRepository : IBookingRepository<Booking>
    {

        private readonly Context _db;

        public BookingRepository(Context db)
        {
            _db = db;
        }

        public Booking Create(Booking entity)
        {
            var schema = this.MapToSchema(entity);

            _db.Booking.InsertOne(schema);

            return this.MapToModel(schema);
        }

        public List<Booking> GetByFilter(HotelMetadataFilter metadata)
        {
            var filter = Builders<BookingSchema>.Filter.In(x => x.HotelId, metadata.HotelsId);

            filter &= (Builders<BookingSchema>.Filter.Gte(x => x.StartDate, metadata.StartDate));
            filter &= (Builders<BookingSchema>.Filter.Lte(x => x.EndDate, metadata.EndDate));

            var result = _db.Booking.Find(filter);

            List<Booking> bookings = new List<Booking>();

            result.ToList().ForEach((schema) =>
            {
                bookings.Add(this.MapToModel(schema));
            });

            return bookings;
        }

        public List<Booking> GetByHotel(string hotelId)
        {
            var filter = Builders<BookingSchema>.Filter.Eq(x => x.HotelId, hotelId);

            var result = _db.Booking.Find(filter);

            List<Booking> bookings = new List<Booking>();

            result.ToList().ForEach((schema) =>
            {
                bookings.Add(this.MapToModel(schema));
            });

            return bookings;
        }

        public Booking? GetById(string hotelId, string bookingId)
        {
            var filter = Builders<BookingSchema>.Filter.Eq(x => x.Id, new ObjectId(bookingId));
            filter &= (Builders<BookingSchema>.Filter.Gte(x => x.HotelId, hotelId));

            var result = _db.Booking.Find(filter).FirstOrDefault();

            if (result == null) return null;

            return this.MapToModel(result);
        }

        private BookingSchema MapToSchema(Booking booking)
        {
            return new()
            {
                Id = new ObjectId(),
                HotelId = booking.HotelId,
                RoomId = booking.RoomId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Guests = this.MapGuestToSchema(booking.Guests)
            };
        }

        private List<GuestSubSchema> MapGuestToSchema(List<Guest> guests)
        {
            var result = new List<GuestSubSchema>();

            guests.ForEach((guest) =>
            {
                result.Add(new()
                {
                    FullName = guest.FullName,
                    BirthDate = guest.BirthDate,
                    GenderType = guest.GenderType,
                    DocumentType = guest.DocumentType,
                    DocumentNumber = guest.DocumentNumber,
                    Mail = guest.Mail,
                    PhoneNumber = guest.PhoneNumber,
                    IsOwnBooking = guest.IsOwnBooking
                });
            });


            return result;
        }

        private Booking MapToModel(BookingSchema schema)
        {
            var guests = this.MapGuestToModel(schema.Guests);

            Booking booking = new Booking(schema.HotelId, schema.RoomId, guests, schema.StartDate, schema.EndDate);

            booking.Id = schema.Id.ToString();

            return booking;
        }

        private List<Guest> MapGuestToModel(List<GuestSubSchema> schemas)
        {
            var guests = new List<Guest>();

            schemas.ForEach((schema) =>
            {
                guests.Add(new()
                {
                    FullName = schema.FullName,
                    BirthDate = schema.BirthDate,
                    GenderType = schema.GenderType,
                    DocumentType = schema.DocumentType,
                    DocumentNumber = schema.DocumentNumber,
                    Mail = schema.Mail,
                    PhoneNumber = schema.PhoneNumber,
                    IsOwnBooking = schema.IsOwnBooking
                });
            });

            return guests;
        }

    }
}

