using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebARS.Models;

namespace ARS_Main.Models
{
    public class SeatTypes
    {
        public int Id { get; set; }
        public int AirplaneId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public Airplane Airplane { get; set; }
    }
}