using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdministration.Models
{
    public class ServiceReservationViewModel
    {
        public int ServiceReservationId { get; set; }
        public int ServiceId { get; set; }
        public double ServiceReservationPrice { get; set; }
    }
}