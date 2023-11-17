using Microsoft.EntityFrameworkCore;
using System;

namespace Hotel.API.Models
{
    public class ContactInformations
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public virtual Hotels Hotel { get; set; }
      
    }
}
