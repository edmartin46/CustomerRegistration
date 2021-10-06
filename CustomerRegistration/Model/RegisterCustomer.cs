using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterCustomer.Model
{
    public class RegisterCustomerModel : IValidatableObject
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 50 characters")]
        public string LastName {get; set;}
        [Required]
        [RegularExpression(@"^[A-Z][A-Z]-[0-9][0-9][0-9][0-9][0-9][0-9]$", ErrorMessage = "Reference number must be in the format XX-999999.")]
        public string ReferenceNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth == null && String.IsNullOrEmpty(Email))
            {
                yield return new ValidationResult(
                    $"You must specify either a date of birth or an email");
            }
        }
    }
}
