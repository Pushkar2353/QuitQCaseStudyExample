using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.IO;

namespace CaseStudyExample.Models
{
    public class Customers : Users
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateOnly DateofBirth { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pin Code is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Pin Code must be exactly 6 digits.")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Pin Code must contain only digits.")]
        public int PinCode { get; set; }

        public virtual Users? Users { get; set; } = null!;


    }
}
