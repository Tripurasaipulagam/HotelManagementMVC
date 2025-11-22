using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementMVC.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount {  get; set; }

        public virtual Room Room { get; set; }
        public virtual Customer Customer { get; set; }



    }
}