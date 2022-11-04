using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdministration.Models
{
    public class ServicesViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
    }
}