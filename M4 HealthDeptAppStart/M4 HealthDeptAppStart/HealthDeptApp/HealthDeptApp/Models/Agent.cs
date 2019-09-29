
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // READ ONLY PROPERTIES

        [NotMapped]
        public string DisplayName
        {
            // CONCATENATE THE FIRST AND LAST NAME
            // INTO A DISPLAY NAME
            get { return $"{FirstName} {LastName}"; }
        }


        [NotMapped]
        public bool IsActive
        {
            // TRUE/FALSE: FALSE IF AGENT IS ON LEAVE OR HAS LEFT DEPARTMENT
            // OTHERWISE TRUE
            get { return OnLongTermLeave == false && SeparationDate == null; }
        }
    }
}
            