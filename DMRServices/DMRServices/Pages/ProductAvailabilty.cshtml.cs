using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMRServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMRServices.Pages
{
    public class ProductAvailabiltyModel : PageModel
    {
        public DMRServicesContext _context;
        public ICollection<Product> Products;
        public ProductAvailabiltyModel(DMRServicesContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Products = _context.Product
                                .OrderBy(x => x.Name)
                                .ToList();
        }
    }
}