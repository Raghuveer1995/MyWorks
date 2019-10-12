using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMRServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMRServices.Pages
{
    public class IndexModel : PageModel
    {
        private DMRServicesContext _context;
        public int TotalOpenCases;
        public int OpenCasesInLastSevenDays;
        public int ClosedCasesInLastSevenDays;
        public int LowPriorityCasesInLastSevenDays;
        public int MediumPriorityCasesInLastSevenDays;
        public int HighPriorityCasesInLastSevenDays;        
        public int NumberOfCasesClosedOnTimeInLastSevenDays;
        public int NumberOfCasesClosedInBufferTimeInLastSevenDays;
        public int NumberOfCasesBreachedInLastSevenDays;
        public IndexModel(DMRServicesContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            TotalOpenCases = _context.Case
                                        .Where(x => x.Status == "Open")
                                        .Count();

            OpenCasesInLastSevenDays = _context.Case
                                            .Where(x => x.Status == "Open" && x.CreatedDate >= DateTime.Today.AddDays(-7))
                                            .Count();

            ClosedCasesInLastSevenDays = _context.Case
                                                .Where(x => x.Status == "Closed" && x.ClosedDate >= DateTime.Today.AddDays(-7))
                                                .Count();

            LowPriorityCasesInLastSevenDays = _context.Case
                                                     .Where(x => x.Priority == "Low" && x.CreatedDate >= DateTime.Today.AddDays(-7))
                                                     .Count();

            MediumPriorityCasesInLastSevenDays = _context.Case
                                                        .Where(x => x.Priority == "Medium" && x.CreatedDate >= DateTime.Today.AddDays(-7))
                                                        .Count();

            HighPriorityCasesInLastSevenDays = _context.Case
                                                        .Where(x => x.Priority == "High" && x.CreatedDate >= DateTime.Today.AddDays(-7))
                                                        .Count();

            NumberOfCasesClosedOnTimeInLastSevenDays = _context.Case
                                                            .Where(x => x.Remarks == "Case Closed in Time" && x.CreatedDate >= DateTime.Today.AddDays(-7))
                                                            .Count();

            NumberOfCasesClosedInBufferTimeInLastSevenDays = _context.Case
                                                            .Where(x => x.Remarks == "Case Closed in Buffer Time" && x.CreatedDate >= DateTime.Today.AddDays(-7))
                                                            .Count();

            NumberOfCasesBreachedInLastSevenDays = _context.Case
                                                            .Where(x => x.Remarks == "Case Closure Breached" && x.CreatedDate >= DateTime.Today.AddDays(-7))
                                                            .Count();
        }
    }
}
