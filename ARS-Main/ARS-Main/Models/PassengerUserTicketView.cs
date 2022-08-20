using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARS_Main.Models
{
    public class PassengerUserTicketView
    {
        public Passenger Passenger { get; set; }

        public List<Ticket> Tickets { get; set; }

        public List<Flight> Flights { get; set; }

        public List<Plane> Planes { get; set; }
    }
}