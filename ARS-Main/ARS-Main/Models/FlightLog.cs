using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebARS.Models
{
    public class FlightLog//NHẬT KÝ CHUYẾN BAY
    {
        public int Id { get; set; }
        public DateTime LogDate { get; set; }
        public string FlightNo { get; set; }
        [ForeignKey("FlightNo")]
        public FlightSchedule FlightSchedule { get; set; }
        public int FlightId { get; set; }
        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }

    }
}