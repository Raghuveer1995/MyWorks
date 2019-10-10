using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMRServices.Models
{
    public class Customer
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        [StringLength(50, MinimumLength = 1)]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        [CustomValidation(typeof(Customer), "CustomerEmailValidation")]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public ICollection<Case> Cases { get; set; }
        public ICollection<Order> Orders { get; set; }

        public static ValidationResult CustomerEmailValidation(string email, ValidationContext context)
        {
            var instance = context.ObjectInstance as Customer;
            var dbContext = context.GetService(typeof(DMRServicesContext)) as DMRServicesContext;
            int existingCustomerCount = dbContext.Customer
                                                        .Count(x => x.ID != instance.ID && x.Email == email);
            if (string.IsNullOrWhiteSpace(email) || instance == null)
            {
                return ValidationResult.Success;
            }
            if (existingCustomerCount > 0)
            {
                return new ValidationResult("Customer with this email already exists");
            }
            return ValidationResult.Success;
        }
    }
}
