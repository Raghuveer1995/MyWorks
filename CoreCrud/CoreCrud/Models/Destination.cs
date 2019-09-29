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
        [Required]
        [StringLength(500, MinimumLength = 1)]
        [CustomValidation(typeof(Destination), "DuplicateDestinationValidation")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        [Display(Name = "Long Vacation Destination")]
        public bool LongVacationDestination { get; set; }
        [Required]
        [Display(Name = "Price Per Person")]
        [Range(1, 800, ErrorMessage = "Price must be in between 1 and 800")]
        [CustomValidation(typeof(Destination), "PriceForUSADestination")]
        public decimal? PricePerPerson { get; set; }
        [Display(Name = "Famous Place")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Length of place cannot exceed 100 characters")]
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


        public static ValidationResult PriceForUSADestination(decimal? priceOfUSADestination, ValidationContext context)
        {
            var instance = context.ObjectInstance as Destination;
            var dbContext = context.GetService(typeof(CoreCrudContext)) as CoreCrudContext;
            var USACountryId = dbContext.Country.FirstOrDefault(x => x.Name == "USA");
            if (instance == null || priceOfUSADestination == null)
            {
                return ValidationResult.Success;
            }
            if (USACountryId == null)
            {
                return ValidationResult.Success;
            }
            if (instance.CountryID == USACountryId.ID && instance.PricePerPerson < 300)
            {
                return new ValidationResult("Price cannot be less than 300 for US Destinations");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult DuplicateDestinationValidation(string nameOfDestination, ValidationContext context)
        {
            if (nameOfDestination == null)
            {
                return ValidationResult.Success;
            }
            var instance = context.ObjectInstance as Destination;
            var dbContext = context.GetService(typeof(CoreCrudContext)) as CoreCrudContext;
            var instanceCountry = dbContext.Country.FirstOrDefault(x => x.ID == instance.CountryID);
            var similarDestinations = dbContext.Destination
                                                    .Count(x => x.ID != instance.ID && x.Name == nameOfDestination && x.Location == instanceCountry);
            if (similarDestinations > 0)
            {
                return new ValidationResult("Destination already exists");
            }
            return ValidationResult.Success;
        }
    }    
}
