using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementMVC.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}