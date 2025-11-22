using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementMVC.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}