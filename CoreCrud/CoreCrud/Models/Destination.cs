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
        public decimal? PricePerPerson { get; set; }
        public string FamousPlace { get; set; }
        public int CountryID { get; set; }
        public Country Location { get; set; }
        public int Rating
        {
            get
            {
                if(PricePerPerson <= 150)
                {
                    return 5;
                }else if (PricePerPerson >150 && PricePerPerson < 250)
                {
                    return 4;
                }else if (PricePerPerson > 250 && PricePerPerson < 350)
                {
                    return 3;
                }else if (PricePerPerson >350 && PricePerPerson < 450)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
