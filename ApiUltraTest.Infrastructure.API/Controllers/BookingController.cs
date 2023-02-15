using ApiUltraTest.Application.Interfaces;
using ApiUltraTest.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using ApiUltraTest.Domain.Models.BookingModel;
using ApiUltraTest.Domain.Location;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUltraTest.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BookingController : Controller
    {
        private readonly IBookingService<BookingDto> _bookingService;

        public BookingController(IBookingService<BookingDto> bookingService)
        {
            _bookingService = bookingService;
        }

        // GET api/values/5
        [MapToApiVersion("1.0")]
        [HttpGet("{hotelId}")]
        public ActionResult<List<BookingDto>> GetByHotel(string hotelId)
        {
            return Ok(_bookingService.GetByHotelId(hotelId));
        }

        // GET api/values/5
        [MapToApiVersion("1.0")]
        [HttpGet("{hotelId}/{bookingId}")]
        public ActionResult<BookingDto> Get(string hotelId, string bookingId)
        {
            return Ok(_bookingService.GetById(hotelId, bookingId));
        }

        // POST api/values
        [MapToApiVersion("1.0")]
        [HttpPost]
        public ActionResult<BookingDto> Post([FromBody] BookingDto bookingDto)
        {

            return Ok(_bookingService.Create(bookingDto));
        }
    }
}

