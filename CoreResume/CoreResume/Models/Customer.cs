using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreResume.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Case> Cases { get; set; }
    }
}
