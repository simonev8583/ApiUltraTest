using System;

namespace ApiUltraTest.Domain.Location
{
    public class City
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public City(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }
    }
}

