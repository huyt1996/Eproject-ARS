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
        [MinLength(3, ErrorMessage = "Min 3 Char Req"), MaxLength(10, ErrorMessage = "Max 10 Char allow")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Min 3 Char Req"), MaxLength(10, ErrorMessage = "Max 10 Char allow")]
        public string Password { get; set; }
    }

    /*[Table("TblUserAccount")]
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
    }*/

    public class Plane
    {
        [Key]
        public int PlaneID { get; set; }

        [Display(Name = "Plane Name")]
        [Required(ErrorMessage = "Plane name req")]
        [MaxLength(30, ErrorMessage = "Max 30 char allowed")]
        public string PlaneName { get; set; }

        [Required]
        [Display(Name = "Plane Number")]
        public String PlaneNumber { get; set; }

        [Required(ErrorMessage = "Capacity req")]
        [Display(Name = "Seating Capacity")]
        [Range(10, 200)]
        public int SeatingCapacity { get; set; }

        public List<Seat> BookedSeats { get; set; }

    }

    public class Flight
    {
        [Key]
        public int ID { get; set; }

        public virtual Plane Plane { get; set; }

        [Required, Display(Name = "Plane No: ")]
        public int PlaneId { get; set; }

        [Required, Display(Name = "From City")]
        public string From { get; set; }

        [Required, Display(Name = "To City")]
        public string To { get; set; }

        [Required, Display(Name = "Departure Date")]
        public string DepDate { get; set; }

        [Required, Display(Name = "Departure Time")]
        public string DepTime { get; set; }

        [Required, Display(Name = "Arrival Date")]
        public string ArDate { get; set; }

        [Required, Display(Name = "Arrival Time")]
        public string ArTime { get; set; }
        
        [Required, Display(Name = "Price: ")]
        public float Price { get; set; }

    }

    public class Seat
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Seat Number")]
        [Range(1, 200)]
        public int SeatNumber { get; set; }

        public virtual Passenger Passenger { get; set; }

        [Required]
        [Display(Name = "Passenger Name")]
        public int PassengerId { get; set; }

        [Required]
        [Display(Name = "Booked Time")]
        public String Time { get; set; }

        public Seat()
        {
            this.IsAvailable = true;
            this.Time = DateTime.Now.ToString();
        }


    }
}