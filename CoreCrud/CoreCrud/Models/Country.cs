using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCrud.Models
{
    public class Country
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "Please provide a Country Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length should be between 1 to 100 charaters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide a Continent Name")]
        public string Continent { get; set; }
        public ICollection<Destination> Destinations { get; set; }
    }
}
