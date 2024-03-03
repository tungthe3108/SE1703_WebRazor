using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1703_WebRazor.Models;

namespace SE1703_WebRazor.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly SE1703_WebRazor.Models.eStoreContext _context;

        public IndexModel(SE1703_WebRazor.Models.eStoreContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;
        [BindProperty]
        public string ProductName { get; set; }
        [BindProperty]
        public string UnitPrice { get; set; }
        [BindProperty]
        public string id { get; set; }
        public async Task OnGetAsync(int id)
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.Category).ToListAsync();
            }
        }

        public async Task OnPostAsync()
        {
            // Filter products based on search criteria
            if (!string.IsNullOrEmpty(ProductName) && !string.IsNullOrEmpty(UnitPrice))
            {
                Product = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => (p.ProductName.Contains(ProductName) || p.ProductName.Equals(ProductName)) && p.UnitPrice.Equals(int.Parse(UnitPrice)))
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(ProductName))
            {
                Product = await _context.Products
                   .Include(p => p.Category)
                   .Where(p => (p.ProductName.Contains(ProductName) || p.ProductName.Equals(ProductName)))
                   .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(UnitPrice))
            {
                Product = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.UnitPrice.Equals(int.Parse(UnitPrice)))
                    .ToListAsync();
            }
            else
            {
                // Fetch no products if any of the search criteria is invalid
                Product = new List<Product>();
            }
        }


    }
}
