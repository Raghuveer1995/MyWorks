using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMRServices.Models
{
    public class CaseClosureForm
    {
        public int CaseId { get; set; }

        [Display(Name = "Closure Date")]
        [DataType(DataType.Date)]
        public DateTime? ClosureDate { get; set; }
    }
}
