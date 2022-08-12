using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebARS.Models
{
    //  LOẠI MÁY BAY
    public class AirplaneType
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Airplane> Airplanes { get; set; }
    }
}