using ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas;
using ApiUltraTest.Infrastructure.Data.Configs.Context;
using ApiUltraTest.Domain.Interfaces.Repository;
using ApiUltraTest.Domain;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using ApiUltraTest.Domain.Models;

namespace ApiUltraTest.Infrastructure.Data.Repositories.HotelRepository
{
    public class HotelRepository : IHotelRepository<Hotel>
    {
        private readonly Context _db;

        public HotelRepository(Context db)
        {
            _db = db;
        }

        public Hotel Create(Hotel entity)
        {
            var schema = this.MapToSchema(entity);

            _db.Hotel.InsertOne(schema);

            return this.MapToModel(schema);
        }

        public Hotel? GetById(string id)
        {
            var result = _db.Hotel.Find(Builders<HotelSchema>.Filter.Eq("_id", new ObjectId(id)));

            if (result == null) return null;

            return MapToModel(result.FirstOrDefault());
        }

        public Hotel ChangeStatus(Hotel entity)
        {
            var filter = Builders<HotelSchema>.Filter.Eq("_id", new ObjectId(entity.Id));
            var query = Builders<HotelSchema>.Update.Set("status", entity.Status);

            var result = _db.Hotel.FindOneAndUpdate(filter, query);

            return MapToModel(result);
        }

        public Hotel Update(Hotel entity)
        {
            var filter = Builders<HotelSchema>.Filter.Eq("_id", new ObjectId(entity.Id));
            var query = Builders<HotelSchema>.Update.Set("name", entity.Name).Set("situation", entity.Situation).Set("status", entity.Status);

            _db.Hotel.FindOneAndUpdate(filter, query);

            var result = _db.Hotel.FindOneAndUpdate(filter, query);

            return MapToModel(result);
        }

        public List<Hotel> GetByFilter(HotelMetadataFilter metadata)
        {

            var filter = Builders<HotelSchema>.Filter.Eq(x => x.Situation.Department.City.Name, metadata.City);

            filter &= (Builders<HotelSchema>.Filter.Eq(x => x.Status, Domain.Enumerable.RoomStatusTypeEnumerable.Available));

            var result = _db.Hotel.Find(filter);

            List<Hotel> resultModels = new List<Hotel>();

            result.ToList().ForEach((hotelSchema) =>
            {
                resultModels.Add(this.MapToModel(hotelSchema));
            });

            return resultModels;
        }

        private HotelSchema MapToSchema(Hotel hotel)
        {
            return new()
            {
                Id = new ObjectId(),
                Name = hotel.Name,
                Status = hotel.Status,
                Situation = hotel.Situation,
            };
        }

        private Hotel MapToModel(HotelSchema schema)
        {

            Hotel hotel = new Hotel(schema.Name, schema.Situation);

            hotel.Id = schema.Id.ToString();
            hotel.Status = schema.Status;
            hotel.Situation = schema.Situation;

            return hotel;
        }
    }
}

