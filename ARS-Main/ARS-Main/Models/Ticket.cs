using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ARS_Main.Models
{
    public class Ticket
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Flight")]
        public Flight Flight { get; set; }

        [Required]
        [Display(Name = "Booking Time")]
        public string BookingTime { get; set; }

        [Required]
        public bool IsBlocked { get; set; }


    }
}