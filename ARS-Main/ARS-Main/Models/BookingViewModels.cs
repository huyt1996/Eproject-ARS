using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ARS_Main.Models
{
    public class BookingViewModel
    {
        public List<Flight> Flights { get; set; }

        public int Flight { get; set; }

        [Display(Name = "Number Of Chairs")]
        public int NumberOfChairs { get; set; }

        public String PaymentMethod { get; set; }

    }

    public class BookingTicket
    {
        public int Flight { get; set; }

        public String PaymentMethod { get; set; }

        public int NumberOfChairs { get; set; }

    }
}