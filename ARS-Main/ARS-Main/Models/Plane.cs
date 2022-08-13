using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARS_Main.Models
{
    [Table("TblAdminLogin")]
    public class AdminLogin
    {
        [Key]
        public int AdminId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User name required")]
        [MinLength(3, ErrorMessage ="Min 3 Char Req"), MaxLength(10, ErrorMessage ="Max 10 Char allow")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Min 3 Char Req"), MaxLength(10, ErrorMessage = "Max 10 Char allow")]
        public string Password { get; set; }
    }

    public class AeroPlaneInfo
    {
        [Key]
        public int Planeid { get; set; }

        [Display(Name = "Aero Plane Name")]
        [Required(ErrorMessage = "Capacity req")]
        public string APlaneName { get; set; }

        [Display(Name = "Seating Capacity")]
        [Required(ErrorMessage = "Aeroplane name req")]
        public string SeatingCapacity { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price req")]
        public float Price { get; set; }
    }

    [Table("TblFlightBook")]
    public class FlightBooking
    {
        [Key]
        public int bid { get; set; }

        [Display(Name = "From City")]
        [Required(ErrorMessage = "From City req")]
        [StringLength(40, ErrorMessage="Max 40 char allowed")]
        public string From { get; set; }

        [Display(Name = "To City")]
        [Required(ErrorMessage = "To City req")]
        [StringLength(40, ErrorMessage = "Max 40 char allowed")]
        public string To { get; set; }

        [Display(Name = "Departure Date")]
        [DataType(DataType.Date)]
        public DateTime DDate { get; set; }

        [Display(Name = "Departure Time")]
        [StringLength(15)]
        public string DTime { get; set; }

        public int Planeid { get; set; }
        public virtual AeroPlaneInfo PlaneInfo { get; set; }

        [Display(Name="Seat Type")]
        [StringLength(25)]
        public string SeatType { get; set; }

    }

}