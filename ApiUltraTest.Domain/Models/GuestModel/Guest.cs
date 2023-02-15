using System;
using ApiUltraTest.Domain.Enumerable;

namespace ApiUltraTest.Domain.Models.GuestModel
{
    public class Guest
    {
        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderTypeEnumerable GenderType { get; set; }

        public DocumentTypeEnumerable DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        public string Mail { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsOwnBooking { get; set; }

        public Guest()
        {
        }
    }
}

