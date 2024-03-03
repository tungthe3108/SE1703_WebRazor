using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1703_WebRazor.Models;

namespace SE1703_WebRazor.Pages.Report
{
    public class OrderDetailsModel : PageModel
    {
        private readonly SE1703_WebRazor.Models.eStoreContext _context;

        public OrderDetailsModel(SE1703_WebRazor.Models.eStoreContext context)
        {
            _context = context;
        }

        public OrderDetail? OrderDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = orderDetail;
            }
            return Page();
        }
    }
}
