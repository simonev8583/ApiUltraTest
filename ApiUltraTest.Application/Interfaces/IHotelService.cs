using System;
using ApiUltraTest.Application.Dtos;

namespace ApiUltraTest.Application.Interfaces
{
    public interface IHotelService<TEntity>
    {
        TEntity Create(TEntity dto);

        TEntity UpdateStatus(String hotelId, int status);

        TEntity Update(TEntity dto, string hotelId);

        List<TEntity> GetByFilter(HotelMetadataFilterDto metadada);
    }
}

