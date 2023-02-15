using ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas;
using ApiUltraTest.Infrastructure.Data.Configs.Context;
using Microsoft.Extensions.Options;
using ApiUltraTest.Domain;
using MongoDB.Driver;
using System;

namespace ApiUltraTest.Infrastructure.Data.Configs.Context
{
    public class Context
    {
        private readonly IMongoCollection<HotelSchema> _hotelCollection;
        private readonly IMongoCollection<RoomSchema> _roomCollection;
        private readonly IMongoCollection<BookingSchema> _bookingCollection;

        public Context(IOptions<GeneralSettings> storeSettings)
        {
            var mongoClient = new MongoClient(
                storeSettings.Value.StringConnection);

            var mongoDb = mongoClient.GetDatabase(storeSettings.Value.DataBaseName);

            _hotelCollection = mongoDb.GetCollection<HotelSchema>(
                storeSettings.Value.HotelCollectionName);

            _roomCollection = mongoDb.GetCollection<RoomSchema>(
                storeSettings.Value.RoomCollectionName);

            _bookingCollection = mongoDb.GetCollection<BookingSchema>(
                storeSettings.Value.BookingCollectionName);
        }

        public IMongoCollection<HotelSchema> Hotel
        {
            get
            {
                return _hotelCollection;
            }
        }

        public IMongoCollection<RoomSchema> Room
        {
            get
            {
                return _roomCollection;
            }
        }

        public IMongoCollection<BookingSchema> Booking
        {
            get
            {
                return _bookingCollection;
            }
        }
    }
}

