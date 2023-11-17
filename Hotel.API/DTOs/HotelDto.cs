using System;
using System.Collections.Generic;

namespace Hotel.API.DTOs
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        public string AuthorizedName { get; set; }
        public string AuthorizedLastName { get; set; }
        public string Company { get; set; }
        public List<ContactInformationDto> contactInformations { get; set; }
    }
}
