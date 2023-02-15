using ApiUltraTest.Domain.Interfaces.Repository;
using ApiUltraTest.Domain;
using System;
using ApiUltraTest.Application.Dtos;
using ApiUltraTest.Domain.Enumerable;

namespace ApiUltraTest.Application.Services.CommonServices
{
    public class RoomCommonService
    {
        private readonly IRoomRepository<Room> _roomProvider;



        public RoomCommonService(IRoomRepository<Room> roomProvider)
        {
            _roomProvider = roomProvider;
        }

        public Room AddRoom(RoomDto roomDto, Hotel hotel)
        {
            if (roomDto == null) throw new ArgumentNullException("No hay habitación para crear");

            Room room = new Room(roomDto.RoomNumber!, roomDto.Location!, hotel.Id!);

            room.CostBase = roomDto.CostBase ?? 0;
            room.Tax = roomDto.Tax ?? 0;
            room.RoomType = (RoomTypeEnumerable)roomDto.RoomType!;
            room.Status = (RoomStatusTypeEnumerable)roomDto.Status!;
            room.Capacity = roomDto.Capacity;

            Room roomStored = _roomProvider.Insert(room);

            return roomStored;
        }
    }
}

