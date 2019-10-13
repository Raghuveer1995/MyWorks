using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMRServices.Models
{
    public class CaseClosureForm
    {
        public int CaseId { get; set; }

        [Display(Name = "Closure Date")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CaseClosureForm), "CloseDateValidation")]
        public DateTime? ClosureDate { get; set; }

        public static ValidationResult CloseDateValidation(DateTime? dateValue, ValidationContext context)
        {
            if (dateValue == null)
            {
                return ValidationResult.Success;
            }
            if (dateValue > DateTime.Today)
            {
                return new ValidationResult("Closed Date cannot be a Future Date");
            }
            return ValidationResult.Success;
        }
    }

    
}
