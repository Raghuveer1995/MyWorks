using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreResume.Models
{
    public class Case
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
