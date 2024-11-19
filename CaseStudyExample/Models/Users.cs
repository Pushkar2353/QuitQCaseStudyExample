using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CaseStudyExample.Models
{
    public class Users
    {
        /* public Users()
         {
             Cart = new HashSet<Cart>();
             Orders = new HashSet<Orders>();
             Payments = new HashSet<Payments>();
             Reviews = new HashSet<Reviews>();
         }
        */
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Password is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The Password must be between 8 and 20 characters.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
    ErrorMessage = "The Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "UserType is required.")]
        [EnumDataType(typeof(UserType))]
        public UserType UserType { get; set; }

        public virtual Customers? Customers { get; set; } = null!;
        public virtual Sellers? Sellers { get; set; } = null!;
        public virtual Administrator? Administrator { get; set; } = null!;
        public virtual ICollection<Cart>? Cart { get; set; }
        public virtual ICollection<Orders>? Orders { get; set; }
        public virtual ICollection<Payments>? Payments { get; set; }
        public virtual ICollection<Reviews>? Reviews { get; set; }

    }

    public enum UserType
    {
        Customer,
        Seller,
        Admin
    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
