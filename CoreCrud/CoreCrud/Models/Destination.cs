using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCrud.Models
{
    public class Destination
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        public bool LongVacationDestination { get; set; }
        public decimal PricePerPerson { get; set; }
        public string FamousPlace { get; set; }
        public string CountryID { get; set; }
        public Country Country { get; set; }
    }
}
