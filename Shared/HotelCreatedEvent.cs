using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class HotelCreatedEvent
    {
        public Guid Id { get; set; }
        public string AuthorizedName { get; set; }
        public string AuthorizedLastName { get; set; }
        public string Company { get; set; }
        public List<ContactInformationMessage> ContactInformations { get; set; } = new List<ContactInformationMessage>();
    }
}
