using ApiUltraTest.Application.Interfaces;
using ApiUltraTest.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiUltraTest.Domain;
using System.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUltraTest.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class HotelController : Controller
    {

        private readonly IHotelService<HotelDto> _hotelService;

        public HotelController(IHotelService<HotelDto> hotelService)
        {
            _hotelService = hotelService;
        }

        // POST api/values
        [MapToApiVersion("1.0")]
        [HttpPost]
        public ActionResult<HotelDto> Post([FromBody] HotelDto hotelDto)
        {
            return Ok(_hotelService.Create(hotelDto));
        }

        //PATCH
        [MapToApiVersion("1.0")]
        [HttpPatch("{hotelId}/status/{newStatus}")]
        public ActionResult<HotelDto> ChangeStatus(string hotelId, int newStatus)
        {
            return Ok(_hotelService.UpdateStatus(hotelId, newStatus));
        }

        // PUT api/values/5
        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        public ActionResult<HotelDto> Put(string id, [FromBody] HotelDto hotelDto)
        {
            return Ok(_hotelService.Update(hotelDto, id));
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public ActionResult<List<HotelDto>> Get(string? startDate, string? endDate, int quantityPerson, string? city)
        {
            List<KeyValuePair<string, string?>> parameters = new List<KeyValuePair<string, string?>>()
            {
                new KeyValuePair<string, string?>("StartDate", startDate),
                new KeyValuePair<string, string?>("EndDate", endDate),
                new KeyValuePair<string, string?>("QuantityPerson", quantityPerson.ToString()),
                new KeyValuePair<string, string?>("City", city),
            };

            HotelMetadataFilterDto metadata = HotelMetadataFilterDto.GetByQueryString(parameters);

            return Ok(_hotelService.GetByFilter(metadata));
        }
    }
}

