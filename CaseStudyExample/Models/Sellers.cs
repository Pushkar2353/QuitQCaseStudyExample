using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyExample.Models
{
    public class Sellers : Users
    {
        /*
        public Sellers()
        {
            Products = new HashSet<Products>();
        }
        */
        [Key]
        public int SellerId { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public int SellerPhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Address is required.")]
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

        [Required(ErrorMessage = "GSTIN is required.")]
        [StringLength(15, ErrorMessage = "The GSTIN must be 15 characters long.")]
        [RegularExpression("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[A-Z0-9]{1}[Z]{1}[A-Z0-9]{1}$", ErrorMessage = "Invalid GSTIN format.")]
        public string GSTIN { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Bank Account Number is required.")]
        [StringLength(18, MinimumLength = 9, ErrorMessage = "The Bank Account Number must be between 9 and 18 digits.")]
        [RegularExpression("^[0-9]{9,18}$", ErrorMessage = "The Bank Account Number must contain only digits.")]
        public string BankAccountNumber { get; set; } = string.Empty;

        public virtual Users? Users { get; set; } = null!;
        public virtual ICollection<Products>? Products { get; set; }

    }
}
