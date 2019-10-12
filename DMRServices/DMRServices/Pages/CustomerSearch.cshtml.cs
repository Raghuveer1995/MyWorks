using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMRServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DMRServices.Pages
{
    public class CustomerSearchModel : PageModel
    {
        private DMRServicesContext _context;
        public bool SearchCompletion { get; set; }
        [BindProperty]
        public string Search { get; set; }
        public ICollection<Customer> RetrievedCustomers;
        public CustomerSearchModel(DMRServicesContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            SearchCompletion = false;
        }
        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(Search))
            {
                return;
            }
            RetrievedCustomers = _context.Customer
                                            .Include(x => x.Cases)
                                            .Include(x => x.Orders)
                                            .Where(x => x.LastName.ToLower().Contains(Search.ToLower()))
                                            .ToList();
            SearchCompletion = true;
        }
    }
}