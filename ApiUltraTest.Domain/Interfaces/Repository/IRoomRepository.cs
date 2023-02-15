using System;
using ApiUltraTest.Domain.Models;

namespace ApiUltraTest.Domain.Interfaces.Repository
{
    public interface IRoomRepository<TEntity>
    {
        List<TEntity> BulkInsert(List<TEntity> entities);

        TEntity Insert(TEntity entity);

        TEntity? GetById(string id);

        TEntity? GetByIdAndHotel(string roomId, string hotelId);

        TEntity ChangeStatus(TEntity entity);

        TEntity Update(TEntity entity);

        List<TEntity>? GetByHotel(string hotelId);

    }
}

