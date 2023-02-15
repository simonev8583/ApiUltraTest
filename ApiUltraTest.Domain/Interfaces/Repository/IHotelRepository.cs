using System;
using ApiUltraTest.Domain.Models;

namespace ApiUltraTest.Domain.Interfaces.Repository
{
    public interface IHotelRepository<TEntity>
    {
        TEntity Create(TEntity entity);

        TEntity? GetById(string id);

        TEntity ChangeStatus(TEntity entity);

        TEntity Update(TEntity entity);

        List<TEntity> GetByFilter(HotelMetadataFilter metadata);
    }
}

