using ApiUltraTest.Application.Services.CommonServices;
using ApiUltraTest.Domain.Interfaces.Repository;
using ApiUltraTest.Domain.Models.BookingModel;
using ApiUltraTest.Application.Interfaces;
using ApiUltraTest.Domain.Enumerable;
using ApiUltraTest.Application.Dtos;
using ApiUltraTest.Domain;
using System;

namespace ApiUltraTest.Application.Services
{
    public class HotelService : IHotelService<HotelDto>
    {
        private readonly IHotelRepository<Hotel> _hotelProvider;
        private readonly IRoomRepository<Room> _roomProvider;
        private readonly RoomCommonService _roomCommonService;
        private readonly IBookingRepository<Booking> _bookingProvider;

        public HotelService(IHotelRepository<Hotel> hotelProvider, IRoomRepository<Room> roomProvider, IBookingRepository<Booking> bookingRepository)
        {
            _hotelProvider = hotelProvider;
            _roomProvider = roomProvider;
            _bookingProvider = bookingRepository;

            _roomCommonService = new RoomCommonService(_roomProvider);
        }

        public HotelDto Create(HotelDto dto)
        {
            if (dto == null) throw new ArgumentNullException("La información del Hotel es requerida");

            Hotel hotel = HotelDto.ToDomain(dto);

            Hotel hotelStored = _hotelProvider.Create(hotel);

            HotelDto hotelResult = HotelDto.FromDomain(hotelStored);

            if (!dto.Rooms!.Any())
            {
                return hotelResult;
            }

            dto.Rooms?.ForEach((roomDto) =>
            {
                var roomStored = _roomCommonService.AddRoom(roomDto, hotelStored);

                hotelResult?.Rooms?.Add(RoomDto.FromDomain(roomStored));
            });

            return hotelResult;
        }

        public List<HotelDto> GetByFilter(HotelMetadataFilterDto metadataDto)
        {
            var metadata = HotelMetadataFilterDto.ToDomain(metadataDto);

            List<Hotel> hotels = _hotelProvider.GetByFilter(metadata);

            metadata.HotelsId = new List<string>();

            hotels.ForEach((hotel) =>
            {
                metadata.HotelsId.Add(hotel.Id!);
            });

            List<Booking> bookings = _bookingProvider.GetByFilter(metadata);

            List<Room> rooms = new List<Room>();

            hotels.ForEach((hotel) =>
            {
                var listRooms = _roomProvider.GetByHotel(hotel.Id!);
                if (listRooms!.Any())
                {
                    rooms.AddRange(listRooms!);
                }
            });

            var roomsAvailables = rooms.Where(room => bookings.All(booking => booking.RoomId != room.Id)).ToList();

            var hotelsAvailables = hotels.Where(hotel => roomsAvailables.All(room => room.HotelId == hotel.Id)).ToList();

            var result = new List<HotelDto>();

            hotelsAvailables.ForEach((hotel) =>
            {
                result.Add(HotelDto.FromDomain(hotel));
            });

            return result;
        }

        public HotelDto Update(HotelDto dto, string hotelId)
        {
            if (dto == null) throw new ArgumentNullException("La información del Hotel es requerida");

            Hotel? hotel = _hotelProvider.GetById(hotelId);

            if (hotel == null) throw new KeyNotFoundException("No se encontró el hotel con esa referencia");

            hotel.Name = dto.Name ?? hotel.Name;
            hotel.Situation.Address = dto.Situation?.Address ?? hotel.Situation.Address;
            hotel.Situation.Department = dto.Situation?.Department ?? hotel.Situation.Department;
            hotel.Status = dto.Status ?? hotel.Status;


            Hotel hotelUpdated = _hotelProvider.Update(hotel);

            return HotelDto.FromDomain(hotelUpdated);

        }

        public HotelDto UpdateStatus(string hotelId, int status)
        {
            if (hotelId == null || status < 0) throw new ArgumentNullException("La información del Hotel es requerida");

            Hotel? hotel = _hotelProvider.GetById(hotelId);

            if (hotel == null) throw new KeyNotFoundException("No se encontró el hotel con esa referencia");

            hotel.Status = (RoomStatusTypeEnumerable)status;

            Hotel hotelUpdated = _hotelProvider.ChangeStatus(hotel);

            return HotelDto.FromDomain(hotelUpdated);
        }
    }
}

