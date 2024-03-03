using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1703_WebRazor.Models;

namespace SE1703_WebRazor.Pages.Report
{
    public class ReportModel : PageModel
    {
        private readonly SE1703_WebRazor.Models.eStoreContext _context;

        public ReportModel(SE1703_WebRazor.Models.eStoreContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; } = default!;

        [BindProperty]
        public DateTime? FromDate { get; set; } = System.DateTime.Now;
        [BindProperty]
        public DateTime? ToDate { get; set; } = System.DateTime.Now;
        [BindProperty]
        public string SortDirection { get; set; } = "asc";

        private static DateTime? _lastFromDate = System.DateTime.Now;
        private static DateTime? _lastToDate = System.DateTime.Now;
        private static string _lastSortDirection = "asc";

        //DateTime? FromDate, DateTime? ToDate

        public async Task OnGetAsync()
        {
            FromDate = _lastFromDate;
            ToDate = _lastToDate;
            SortDirection = _lastSortDirection;
            if (_context.Orders != null)
            {
                Orders = await _context.Orders
                    .Where(o => o.OrderDate >= FromDate && o.OrderDate <= ToDate).ToListAsync();
                if (SortDirection == "asc")
                {
                    Orders = Orders.OrderBy(o => o.OrderDate).ToList();
                }
                else if (SortDirection == "desc")
                {
                    Orders = Orders.OrderByDescending(o => o.OrderDate).ToList();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ToDate < FromDate)
            {
                FromDate = _lastFromDate;
                ToDate = _lastToDate;
            }
            if (ToDate > System.DateTime.Now)
                ToDate = _lastToDate;
            _lastFromDate = FromDate;
            _lastToDate = ToDate;
            _lastSortDirection = SortDirection;
            return await Task.FromResult(RedirectToPage());
        }
    }
}
