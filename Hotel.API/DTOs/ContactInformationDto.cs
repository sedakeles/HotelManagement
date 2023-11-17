using System;

namespace Hotel.API.DTOs
{
    public class ContactInformationDto
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
