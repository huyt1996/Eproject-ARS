using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebARS.Models
{
    public class User
    {
        [Display(Name = "User ID")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "You must provide a first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must provide a last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You must provide an address")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "You must provide an email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Are you an existing Customer?")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        public bool Sex { get; set; }
        [Required(ErrorMessage = "You must provide your age")]
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "You must provide a credit card number")]
        [Display(Name = "Credit card number")]
        public string Credit { get; set; }
        [Display(Name = "Sky Miles")]
        public int SkyMile { get; set; }
    }
}