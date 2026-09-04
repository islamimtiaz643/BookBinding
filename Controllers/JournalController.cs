using BookBinding.Data;
using BookBinding.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookBinding.Controllers
{
    public class JournalController : Controller
    {
        private readonly AppDbContext _context;

        public JournalController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, string? leatherColor, string? paperDesign, string? bindingStyle, string sort = "newest")
        {
            var query = _context.Journals
                .Include(j => j.Paper)
                .Include(j => j.Leather)
                .Include(j => j.BindingStyle)
                .Include(j => j.Images)
                .Where(j => j.Status == JournalStatus.Active)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(j => j.Title.Contains(search) || (j.Description != null && j.Description.Contains(search)));
            }

            if (!string.IsNullOrWhiteSpace(leatherColor))
            {
                query = query.Where(j => j.Leather.ColorName == leatherColor);
            }

            if (!string.IsNullOrWhiteSpace(paperDesign))
            {
                query = query.Where(j => j.Paper.DesignType == paperDesign);
            }

            if (!string.IsNullOrWhiteSpace(bindingStyle))
            {
                query = query.Where(j => j.BindingStyle.Name == bindingStyle);
            }

            query = sort switch
            {
                "price_low" => query.OrderBy(j => j.Price),
                "price_high" => query.OrderByDescending(j => j.Price),
                _ => query.OrderByDescending(j => j.CreatedAt)
            };

            ViewBag.LeatherColors = await _context.Leathers.Where(l => l.IsActive)
                .Select(l => l.ColorName).Distinct().ToListAsync();
            ViewBag.PaperDesigns = await _context.Papers.Where(p => p.IsActive)
                .Select(p => p.DesignType).Distinct().ToListAsync();
            ViewBag.BindingStyles = await _context.BindingStyles.Where(b => b.IsActive)
                .Select(b => b.Name).Distinct().ToListAsync();

            var journals = await query.ToListAsync();
            return View(journals);
        }

        public async Task<IActionResult> Details(int id)
        {
            var journal = await _context.Journals
                .Include(j => j.Paper)
                .Include(j => j.Leather)
                .Include(j => j.BindingStyle)
                .Include(j => j.Images)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (journal == null) return NotFound();

            return View(journal);
        }
    }
}