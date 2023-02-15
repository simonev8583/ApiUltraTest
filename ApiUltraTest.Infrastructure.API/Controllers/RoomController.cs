using ApiUltraTest.Application.Interfaces;
using ApiUltraTest.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiUltraTest.Domain;
using System.Linq;
using System;
using ApiUltraTest.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUltraTest.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RoomController : Controller
    {
        private readonly IRoomService<RoomDto> _roomService;

        public RoomController(IRoomService<RoomDto> roomService)
        {
            _roomService = roomService;
        }

        //GET
        [MapToApiVersion("1.0")]
        [HttpGet]
        public ActionResult<List<BookingDto>> GetAvailables(string hotelId, string startDate, string endDate)
        {
            List<KeyValuePair<string, string?>> parameters = new List<KeyValuePair<string, string?>>()
            {
                new KeyValuePair<string, string?>("HotelId", hotelId),
                new KeyValuePair<string, string?>("StartDate", startDate),
                new KeyValuePair<string, string?>("EndDate", endDate),
            };

            HotelMetadataFilterDto metadata = HotelMetadataFilterDto.GetByQueryString(parameters);

            return Ok(_roomService.GetRoomsAvailables(metadata));
        }

        // POST api/values
        [MapToApiVersion("1.0")]
        [HttpPost]
        public ActionResult<RoomDto> Post([FromBody] RoomDto roomDto)
        {
            return Ok(_roomService.Create(roomDto));
        }

        //PATCH
        [MapToApiVersion("1.0")]
        [HttpPatch("{roomId}/status/{newStatus}")]
        public ActionResult<RoomDto> ChangeStatus(string roomId, int newStatus)
        {
            return Ok(_roomService.UpdateStatus(roomId, newStatus));
        }

        // PUT api/values/5
        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        public ActionResult<RoomDto> Put(string id, [FromBody] RoomDto roomDto)
        {
            return Ok(_roomService.Update(roomDto, id));
        }
    }
}

