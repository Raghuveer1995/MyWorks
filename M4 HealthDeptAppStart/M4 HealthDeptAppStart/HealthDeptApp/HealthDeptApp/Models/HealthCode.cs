
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthDeptApp.Models
{
    public class HealthCode
    {
        [Key]
        public int Id { get; set; }
        public int Position { get; set; }
        public string Category { get; set; }
        public int CategoryPosition { get; set; }
        public string Regulation { get; set; }
    }
}
            