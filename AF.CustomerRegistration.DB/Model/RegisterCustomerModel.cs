using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AF.CustomerRegistration.DB.Model
{
    public class RegisterCustomerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength((50))]
        public string FirstName { get; set; }
        [Required]
        [StringLength((50))]
        public string LastName { get; set; }
        [Required]
        [StringLength((9))]        
        public string ReferenceNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
    }
}
