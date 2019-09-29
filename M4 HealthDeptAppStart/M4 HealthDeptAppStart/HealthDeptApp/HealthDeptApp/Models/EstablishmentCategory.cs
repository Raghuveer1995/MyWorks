
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthDeptApp.Models
{
    public class EstablishmentCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int InspectionFrequency { get; set; }
        public bool CanHaveLiquorLicense { get; set; }
        public ICollection<Establishment> Establishments { get; set; }
    }
}
            