using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebARS.Models
{
    //  CHUYẾN BAY
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNo { get; set; }//chuyến bay số

        public int AirplaneId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public Airplane Airplane { get; set; }
        public virtual ICollection<FlightSchedule> FlightSchedules { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        //public virtual ICollection<Booking> Bookings { get; set; }
    }
}