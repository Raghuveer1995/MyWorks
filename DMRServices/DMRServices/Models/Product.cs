using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMRServices.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string Name { get; set; }
        public string Category { get; set; }
        [Required]
        [Range(1,1000)]
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public ICollection<OrderLineItem> OrderLineItems { get; set; }

        public string Availability
        {
            get
            {
                if (InStock == true)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string PriceGroup
        {
            get
            {
                if (Price <= 100)
                {
                    return "Low Cost";
                }
                else if (Price > 100 && Price <= 300)
                {
                    return "Medium Cost";
                }
                else if (Price > 300 && Price <= 600)
                {
                    return "Affordable";
                }
                else if (Price > 600 && Price <= 800)
                {
                    return "Expensive";
                }
                else
                {
                    return "Most Expensive";
                }
            }
        }
    }
}
