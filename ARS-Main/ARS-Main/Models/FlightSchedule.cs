using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebARS.Models
{
    // LỊCH BAY
    public class FlightSchedule
    {
        [Key]
        public string FlightNo { get; set; }//chuyến bay số
        public int FlightId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public Flight Flight { get; set; }

    }
}