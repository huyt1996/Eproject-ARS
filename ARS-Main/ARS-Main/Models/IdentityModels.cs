using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ARS_Main.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "UserRole")]
        public string UserRole { get; set; }

        [Required(ErrorMessage = "You must provide a User Name")]
        [Display(Name = "User Name")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "You must provide a first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must provide a last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must provide an address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "You must provide your age")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "You must provide a credit card number")]
        [Display(Name = "Credit card number")]
        [CreditCard]
        public string CreditCardNumber { get; set; }


        [Display(Name = "Sky Miles")]
        public int SkyMile { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public enum Gender
    {
        Male,
        Female
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        //public DbSet<AdminLogin> AdminLogins { get; set; }
        //public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}