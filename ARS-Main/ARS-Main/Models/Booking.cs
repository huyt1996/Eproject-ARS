using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebARS.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public string Seat { get; set; }
        public float Price { get; set; }
        public int CustomerId { get; set; }
        public Passenger Passenger { get; set; }
        public Flight Flight { get; set; }
    }
}