using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebARS.Models
{
    //  MÁY BAY
    public class Airplane
    {
        public int Id { get; set; }
        
        public int Capacity { get; set; }//Sức chứa
        public AirplaneType AirplaneType { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}