using System;
namespace ApiUltraTest.Domain.Location
{
    public class Situation
    {
        public Department Department { get; set; }

        public string Address { get; set; }

        public Situation(string address, Department department)
        {
            Department = department;
            Address = address;
        }
    }
}

