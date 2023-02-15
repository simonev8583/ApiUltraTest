using ApiUltraTest.Application.Services.CommonServices;
using ApiUltraTest.Domain.Interfaces.Repository;
using ApiUltraTest.Application.Interfaces;
using ApiUltraTest.Application.Dtos;
using ApiUltraTest.Domain;
using System;
using ApiUltraTest.Domain.Enumerable;
using ApiUltraTest.Domain.Models.BookingModel;

namespace ApiUltraTest.Application.Services
{
    public class RoomService : IRoomService<RoomDto>
    {
        private readonly IRoomRepository<Room> _roomProvider;
        private readonly IHotelRepository<Hotel> _hotelProvider;
        private readonly RoomCommonService _roomCommonService;
        private readonly IBookingRepository<Booking> _bookingProiver;

        public RoomService(IRoomRepository<Room> roomRepository, IHotelRepository<Hotel> hotelRepository, IBookingRepository<Booking> bookingRepository)
        {
            _roomProvider = roomRepository;
            _roomCommonService = new RoomCommonService(_roomProvider);
            _hotelProvider = hotelRepository;
            _bookingProiver = bookingRepository;
        }

        public RoomDto Create(RoomDto dto)
        {
            if (dto == null) throw new ArgumentNullException("La información de la habitación es requerida");

            if (dto.HotelId == null) throw new ArgumentNullException("La referencia del Hotel es requerida");

            Hotel? hotel = _hotelProvider.GetById(dto.HotelId);

            if (hotel == null) throw new KeyNotFoundException("No se encontró el hotel con esa referencia");

            Room room = _roomCommonService.AddRoom(dto, hotel!);

            return RoomDto.FromDomain(room);
        }

        public RoomDto Update(RoomDto dto, string roomId)
        {
            if (dto == null) throw new ArgumentNullException("La información de la habitación es requerida");

            Room? room = _roomProvider.GetById(roomId);

            if (room == null) throw new KeyNotFoundException("No se encontró la habitación con esa referencia");

            room.RoomNumber = dto.RoomNumber ?? room.RoomNumber;
            room.CostBase = dto.CostBase ?? room.CostBase;
            room.Tax = dto.Tax ?? room.Tax;
            room.RoomType = dto.RoomType ?? room.RoomType;
            room.Location.FloorNumber = dto.Location?.FloorNumber ?? room.Location.FloorNumber;
            room.Location.Description = dto.Location?.Description ?? room.Location.Description;
            room.Status = dto.Status ?? room.Status;


            Room roomUpdated = _roomProvider.Update(room);

            return RoomDto.FromDomain(roomUpdated);
        }

        public RoomDto UpdateStatus(string roomId, int status)
        {
            if (roomId == null || status < 0) throw new ArgumentNullException("La información del Hotel es requerida");

            Room? room = _roomProvider.GetById(roomId);

            if (room == null) throw new KeyNotFoundException("No se encontró el hotel con esa referencia");

            room.Status = (RoomStatusTypeEnumerable)status;

            _roomProvider.ChangeStatus(room);


            return RoomDto.FromDomain(room);
        }

        public List<RoomDto> GetRoomsAvailables(HotelMetadataFilterDto metadataDto)
        {
            var metadata = HotelMetadataFilterDto.ToDomain(metadataDto);

            Hotel? hotel = _hotelProvider.GetById(metadata.HotelId!);

            if (hotel == null) throw new KeyNotFoundException("No se encontró el hotel con esa referencia");

            var rooms = _roomProvider.GetByHotel(hotel.Id!);

            metadata.HotelsId = new List<string>();

            metadata.HotelsId.Add(hotel.Id!);

            var bookings = _bookingProiver.GetByFilter(metadata);

            var roomsAvailables = rooms!.Where(room => bookings.All(booking => booking.RoomId != room.Id)).ToList();

            var result = new List<RoomDto>();

            roomsAvailables.ForEach((room) =>
            {
                result.Add(RoomDto.FromDomain(room));
            });

            return result;
        }
    }
}

