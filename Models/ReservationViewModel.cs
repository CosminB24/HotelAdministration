using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdministration.Models
{
    public class ReservationViewModel
    {
        public int ResId { get; set; }
        public int PersId { get; set; }
        public int RNumber { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int ReservationPrice { get; set; }

        public PersonViewModel PersonId { get; set; }
        public ROOMViewModel RoomNumber { get; set; }
        public ICollection<ROOMViewModel> Person { get; set; }

    }
}