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

    [Table("TblUserAccount")]
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "First Name is required"), MinLength(3), StringLength(20)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required"), MinLength(3), StringLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required"), MinLength(3), StringLength(20), RegularExpression(@"^\([a-zA-z0-9 \.\&\'\-]+)$", ErrorMessage = "Must be alpha numeric")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password), MinLength(3), StringLength(20)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Age is required"), Range(18, 150)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Phone number is required"), RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Phone number is invalid")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Cnic  is required"), RegularExpression(@"^([0-9]{11})$", ErrorMessage = "CNIC NUM is incorrect")]
        public string Cnic { get; set; }
    }

    public class AeroPlaneInfo
    {
        [Key]
        public int PlaneID { get; set; }

        [Display(Name = "Aero Plane Name")]
        [Required(ErrorMessage = "Aeroplane name req")]
        [MaxLength(30, ErrorMessage = "Max 30 char allowed")]
        public string APlaneName { get; set; }

        [Display(Name = "Seating Capacity")]
        [Required(ErrorMessage = "Capacity req")]
        public int SeatingCapacity { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price req")]
        public float Price { get; set; }

        public virtual ICollection<TicketReserve_tbl> TicketReserve_Tbls { get; set; }
    }

    [Table("TblFlightBook")]
    public class FlightBooking
    {
        [Key]
        public int Bid { get; set; }

        [Required, Display(Name = "Customer Name")]
        public string bCusName { get; set; }

        [Required, Display(Name = "Customer Address")]
        public string bCusAddress { get; set; }

        [Required, Display(Name = "Customer Email")]
        public string bCusEmail { get; set; }

        [Required, Display(Name = "No Of Seats")]
        public string bCusSeats { get; set; }

        [Required, Display(Name = "Customer Phone Number")]
        public string bCusPhoneNum { get; set; }

        [Required, Display(Name = "CNIC")]
        public string bCusCnic { get; set; }

        public int ResID { get; set; }

        public virtual ICollection<TicketReserve_tbl> TicketReserve_Tbls { get; set; }

        /*[Display(Name = "From City")]
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
        public string SeatType { get; set; }*/
    }
    public class TicketReserve_tbl
    {
        [Key]
        public int ResID { get; set; }

        [Required, Display(Name = "From City")]
        public string ResFrom { get; set; }

        [Required, Display(Name = "To City")]
        public string ResTo { get; set; }

        [Required, Display(Name = "Departure Date")]
        public string ResDepDate { get; set; }

        [Required, Display(Name = "Flight Time")]
        public string ResTime { get; set; }

        [Required, Display(Name = "Plane No: ")]
        public string PlaneId { get; set; }

        public virtual AeroPlaneInfo Plane_tbls { get; set; }

        [Required, Display(Name = "Seats Available")]
        public int PlaneSeat { get; set; }

        [Required, Display(Name = "Price: ")]
        public float ResTicketPrice { get; set; }

    }

}