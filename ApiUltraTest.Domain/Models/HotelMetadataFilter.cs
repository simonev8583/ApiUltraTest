using System;
namespace ApiUltraTest.Domain.Models
{
    public class HotelMetadataFilter
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? QuantityPerson { get; set; }

        public string? City { get; set; }

        public string? HotelId { get; set; }

        public List<string>? HotelsId { get; set; }

        public List<KeyValuePair<string, string>> GetScriptData()
        {
            var result = new List<KeyValuePair<string, string>>();

            if (StartDate != null) result.Add(new KeyValuePair<string, string>("startDate", StartDate!.ToString() ?? ""));

            if (EndDate != null) result.Add(new KeyValuePair<string, string>("endDate", EndDate!.ToString() ?? ""));

            if (QuantityPerson != null) result.Add(new KeyValuePair<string, string>("maxCapacity", QuantityPerson!.ToString() ?? ""));

            if (City != null) result.Add(new KeyValuePair<string, string>("situation.Department.City.Name", City ?? ""));

            return result;

        }
    }
}

