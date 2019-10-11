using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMRServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMRServices.Pages
{
    public class CreateOrderLineItemModel : PageModel
    {
        private DMRServicesContext _context;
        [BindProperty]
        public OrderLineItem orderLineItem { get; set; }
         
        public CreateOrderLineItemModel(DMRServicesContext context)
        {
            _context = context;
        }

        public void OnGet(int? id)
        {
            orderLineItem = new OrderLineItem();
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ID", "Name");
            
            if (id != null)
            {
                orderLineItem.OrderId = id.Value;
            }            
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ID", "Name");
                return Page();
            }
            var ordLineItem = new OrderLineItem();
            ordLineItem.OrderId = orderLineItem.OrderId;
            ordLineItem.ProductId = orderLineItem.ProductId;
            ordLineItem.Quantity = orderLineItem.Quantity;
            _context.OrderLineItem.Add(ordLineItem);
            _context.SaveChanges();
            return RedirectToPage("/Orders/Details", new { Id = ordLineItem.OrderId });
        }

        
    }
}