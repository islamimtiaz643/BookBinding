using BookBinding.Data;
using BookBinding.Models;
using BookBinding.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookBinding.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var featured = await _context.Journals
                .Include(j => j.Images)
                .Include(j => j.Leather)
                .Include(j => j.Paper)
                .Where(j => j.Status == JournalStatus.Active && j.IsFeatured)
                .OrderByDescending(j => j.CreatedAt)
                .Take(6)
                .ToListAsync();

            return View(featured);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}