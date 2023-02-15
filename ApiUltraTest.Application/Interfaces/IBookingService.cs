using System;
namespace ApiUltraTest.Application.Interfaces
{
    public interface IBookingService<TEntity>
    {
        TEntity Create(TEntity entity);

        List<TEntity> GetByHotelId(string hotelId);

        TEntity GetById(string hotelId, string bookinId);

    }
}

