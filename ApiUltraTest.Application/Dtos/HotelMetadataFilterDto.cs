using System;
using ApiUltraTest.Domain.Models;

namespace ApiUltraTest.Application.Dtos
{
    public class HotelMetadataFilterDto
    {

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? QuantityPersons { get; set; }

        public string? City { get; set; }

        public string? HotelId { get; set; }


        public static HotelMetadataFilterDto GetByQueryString(List<KeyValuePair<string, string?>> parameters)
        {
            var metadata = new HotelMetadataFilterDto();

            parameters.ForEach((parameter) =>
            {
                var property = metadata.GetType().GetProperty(parameter.Key);

                property?.SetValue(metadata, Convert.ChangeType(parameter.Value ?? null, property.PropertyType));
            });

            return metadata;
        }

        public static HotelMetadataFilter ToDomain(HotelMetadataFilterDto dto)
        {
            var metadata = new HotelMetadataFilter();

            metadata.StartDate = dto.StartDate;
            metadata.EndDate = dto.EndDate;
            metadata.QuantityPerson = dto.QuantityPersons;
            metadata.City = dto.City;
            metadata.HotelId = dto.HotelId;

            return metadata;
        }
    }
}

