using Hotel.API.Models;
using System;
using System.Collections.Generic;
using static Hotel.API.Models.Hotels;

namespace Hotel.API.DTOs
{
    public class HotelCreateDto
    {
        public string AuthorizedName { get; set; }
        public string AuthorizedLastName { get; set; }
        public string Company { get; set; }
        public List<ContactInformationDto> contactInformations { get; set; }
    }
    
}
