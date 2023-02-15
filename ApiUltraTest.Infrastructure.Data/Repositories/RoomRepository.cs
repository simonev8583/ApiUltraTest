using ApiUltraTest.Infrastructure.Data.Configs.Context.Schemas;
using ApiUltraTest.Infrastructure.Data.Configs.Context;
using ApiUltraTest.Domain.Interfaces.Repository;
using ApiUltraTest.Domain;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using ApiUltraTest.Domain.Models;

namespace ApiUltraTest.Infrastructure.Data.Repositories.RoomRepository
{
    public class RoomRepository : IRoomRepository<Room>
    {
        private readonly Context _db;

        public RoomRepository(Context db)
        {
            _db = db;
        }

        public List<Room> BulkInsert(List<Room> entities)
        {
            throw new NotImplementedException();
        }

        public Room Insert(Room entity)
        {
            var schema = this.MapToSchema(entity);

            _db.Room.InsertOne(schema);

            return this.MapToModel(schema);
        }

        public Room? GetById(string id)
        {
            var result = _db.Room.Find(Builders<RoomSchema>.Filter.Eq("_id", new ObjectId(id)));

            if (result == null) return null;

            return MapToModel(result.FirstOrDefault());
        }

        public Room? GetByIdAndHotel(string roomId, string hotelId)
        {

            var filter = Builders<RoomSchema>.Filter.Eq(x => x.Id, new ObjectId(roomId));

            filter &= (Builders<RoomSchema>.Filter.Eq(x => x.HotelId, hotelId));

            var result = _db.Room.Find(filter);

            if (result == null) return null;

            return MapToModel(result.FirstOrDefault());
        }

        public Room ChangeStatus(Room entity)
        {
            var filter = Builders<RoomSchema>.Filter.Eq("_id", new ObjectId(entity.Id));
            var body = Builders<RoomSchema>.Update.Set("status", entity.Status);

            var result = _db.Room.FindOneAndUpdate(filter, body);

            return MapToModel(result);
        }

        public Room Update(Room entity)
        {
            var filter = Builders<RoomSchema>.Filter.Eq("_id", new ObjectId(entity.Id));
            var query = Builders<RoomSchema>.Update
                .Set("roomNumber", entity.RoomNumber)
                .Set("costBase", entity.CostBase)
                .Set("tax", entity.Tax)
                .Set("roomType", entity.RoomType)
                .Set("location", entity.Location)
                .Set("status", entity.Status);

            _db.Room.FindOneAndUpdate(filter, query);

            var result = _db.Room.FindOneAndUpdate(filter, query);

            return MapToModel(result);
        }

        public List<Room>? GetByHotel(string hotelId)
        {
            var filter = Builders<RoomSchema>.Filter.Eq(x => x.HotelId, hotelId);

            var result = _db.Room.Find(filter);

            var rooms = new List<Room>();

            result.ToList().ForEach((schema) =>
            {
                rooms.Add(this.MapToModel(schema));
            });

            return rooms;
        }

        private RoomSchema MapToSchema(Room room)
        {
            return new()
            {
                Id = new ObjectId(),
                RoomNumber = room.RoomNumber,
                CostBase = room.CostBase,
                Tax = room.Tax,
                RoomType = room.RoomType,
                Location = room.Location,
                HotelId = room.HotelId,
                Capacity = room.Capacity,
            };
        }

        private Room MapToModel(RoomSchema schema)
        {
            Room room = new Room(schema.RoomNumber, schema.Location, schema.HotelId);

            room.Id = schema.Id.ToString();
            room.CostBase = schema.CostBase;
            room.Tax = schema.Tax;
            room.RoomType = schema.RoomType;
            room.HotelId = schema.HotelId;
            room.Capacity = schema.Capacity;

            return room;
        }
    }
}

