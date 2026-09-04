using BookBinding.Data;
using BookBinding.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookBinding.Areas.Customer.Controllers
{
    public class DashboardController : CustomerBaseController
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ViewBag.RequestCount = await _context.CustomJournalRequests
                .CountAsync(r => r.UserId == userId);

            ViewBag.WishlistCount = await _context.Wishlists
                .CountAsync(w => w.UserId == userId);

            ViewBag.PendingRequests = await _context.CustomJournalRequests
                .CountAsync(r => r.UserId == userId && r.Status == RequestStatus.Submitted);

            return View();
        }
    }
}