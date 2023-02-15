using System;
namespace ApiUltraTest.Domain.Location
{
    public class Department
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public City City { get; set; }

        public Department(string name, string code, City city)
        {
            this.Name = name;
            this.Code = code;
            this.City = city;
        }
    }
}

