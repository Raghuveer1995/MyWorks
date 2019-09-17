
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthDeptApp.Models
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? SeparationDate { get; set; }
        public bool OnLongTermLeave { get; set; }
        public ICollection<Inspection> Inspections { get; set; }
    }
}
            