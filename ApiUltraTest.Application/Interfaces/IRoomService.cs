using System;
using ApiUltraTest.Application.Dtos;

namespace ApiUltraTest.Application.Interfaces
{
    public interface IRoomService<TEntity>
    {
        TEntity Create(TEntity dto);

        TEntity UpdateStatus(String roomId, int status);

        TEntity Update(TEntity dto, string roomId);

        List<TEntity> GetRoomsAvailables(HotelMetadataFilterDto metadataDto);
    }
}

