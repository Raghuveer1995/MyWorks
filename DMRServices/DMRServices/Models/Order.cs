using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMRServices.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Required]
        public string Status { get; set; }
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CraetedDate { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Order), "PastDeliveryDateValidation")]
        public DateTime DeliveryDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderLineItem> OrderLineItems { get; set; }

        public static ValidationResult PastDeliveryDateValidation(DateTime? deliveryDate, ValidationContext context)
        {
            if (deliveryDate == null)
            {
                return ValidationResult.Success;
            }
            if (deliveryDate <= DateTime.Today)
            {
                return new ValidationResult("Delivery Date must be a Future Date");
            }
            return ValidationResult.Success;
        }
    }
}
