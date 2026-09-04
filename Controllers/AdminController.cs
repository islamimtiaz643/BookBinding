using BookBinding.Data;
using BookBinding.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookBinding.Controllers
{
    public class AdminController : AdminBaseController
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalJournals = await _context.Journals.CountAsync();
            ViewBag.TotalPapers = await _context.Papers.CountAsync();
            ViewBag.TotalLeathers = await _context.Leathers.CountAsync();
            ViewBag.TotalBindingStyles = await _context.BindingStyles.CountAsync();
            ViewBag.TotalUsers = await _context.Users.CountAsync(u => u.Role == UserRole.Customer);
            ViewBag.PendingRequests = await _context.CustomJournalRequests
                .CountAsync(r => r.Status == RequestStatus.Submitted);

            return View();
        }
    }
}