using Shared;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hotel.API.Models
{
    public class Hotels
    {
        public Guid Id { get; set; }
        public string AuthorizedName{ get; set; }
        public string AuthorizedLastName { get; set; }
        public string Company { get; set; }
        public List<ContactInformations> ContactInformations { get; set; } = new List<ContactInformations>();
       // public InformationType Informations { get; set; }
      //  public string InformationContent { get; set; }
       // public Report Report { get; set; }
        //public enum InformationType
        //{
        //    PhoneNumber=1,
        //    Email=2,
        //    Location=3
        //}
    }
}
