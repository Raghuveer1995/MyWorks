
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthDeptApp.Models
{
    public class Inspection
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime? DateInspected { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }
        public string Report { get; set; }
    }
}
            