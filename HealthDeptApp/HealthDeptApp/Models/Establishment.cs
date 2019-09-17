
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthDeptApp.Models
{
    public class Establishment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool HasLiquorLicense { get; set; }
        public int EmployeeCount { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Latitute { get; set; }
        public double Longitude { get; set; }

        // RELATIONSHIPS
        public ICollection<Inspection> Inspections { get; set; }
        public int CategoryId { get; set; }
        public EstablishmentCategory Category { get; set; }

        // READONLY PROPERTIES

        public bool IsOpen {
            get { return ClosedDate == null; }
        }
    }
}       