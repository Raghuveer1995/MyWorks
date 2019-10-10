using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMRServices.Models
{
    public class Case
    {
        public int ID { get; set; }
        [Required]        
        public string Type { get; set; }
        [Required]
        [CustomValidation(typeof(Case), "CaseClosureValidation")]
        public string Status { get; set; }
        [Required]
        public string Priority { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Closed Date")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Case), "FutureClosedDateValidation")]
        public DateTime? ClosedDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Remarks
        {            
            get
            {                
                if (ClosedDate == null)
                {
                    return "In Progress";
                }
                else
                {
                    int caseClosureValue = (ClosedDate - CreatedDate).Value.Days;
                    if (Priority == "Low")
                    {
                        if (caseClosureValue <= 7)
                        {
                            return "Case Closed in Time";
                        }
                        else if(caseClosureValue == 8)
                        {
                            return "Case Closed in Buffer Time";
                        }
                        else
                        {
                            return "Case Closure breached";
                        }
                    }
                    else if (Priority == "Medium")
                    {
                        if (caseClosureValue <= 5)
                        {
                            return "Case Closed in Time";
                        }
                        else if (caseClosureValue == 6)
                        {
                            return "Case Closed in Buffer Time";
                        }
                        else
                        {
                            return "Case Closure breached";
                        }
                    }
                    else
                    {
                        if (caseClosureValue <= 3)
                        {
                            return "Case Closed in Time";
                        }
                        else if (caseClosureValue == 4)
                        {
                            return "Case Closed in Buffer Time";
                        }
                        else
                        {
                            return "Case Closure breached";
                        }
                    }
                }
            }
        }

        public static ValidationResult CaseClosureValidation(string statusValue, ValidationContext context)
        {
            var instance = context.ObjectInstance as Case;
            if (string.IsNullOrWhiteSpace(statusValue) || instance == null)
            {
                return ValidationResult.Success;
            }
            if (statusValue == "Closed" && instance.ClosedDate == null)
            {
                return new ValidationResult("Closed Date must be provided if Case type is Closed");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult FutureClosedDateValidation(DateTime? closedDate, ValidationContext context)
        {
            if (closedDate == null)
            {
                return ValidationResult.Success;
            }
            if (closedDate > DateTime.Today)
            {                
                return new ValidationResult("Closed date cannot be a Future Date");
            }
            return ValidationResult.Success;
        }
    }
}
