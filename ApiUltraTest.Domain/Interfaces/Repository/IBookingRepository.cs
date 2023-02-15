using System;
using ApiUltraTest.Domain.Models;

namespace ApiUltraTest.Domain.Interfaces.Repository
{
    public interface IBookingRepository<TEntity>
    {
        TEntity Create(TEntity entity);

        List<TEntity> GetByFilter(HotelMetadataFilter metadata);

        List<TEntity> GetByHotel(string hotelId);

        TEntity? GetById(string hotelId, string bookingId);
    }
}

