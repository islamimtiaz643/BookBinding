using BookBinding.Data;
using BookBinding.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookBinding.Controllers
{
    public class WishlistController : CustomerBaseController
    {
        private readonly AppDbContext _context;

        public WishlistController(AppDbContext context)
        {
            _context = context;
        }

        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<IActionResult> Index()
        {
            var items = await _context.Wishlists
                .Include(w => w.Journal)
                .ThenInclude(j => j.Images)
                .Where(w => w.UserId == CurrentUserId)
                .OrderByDescending(w => w.AddedAt)
                .ToListAsync();

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int journalId)
        {
            bool exists = await _context.Wishlists
                .AnyAsync(w => w.UserId == CurrentUserId && w.JournalId == journalId);

            if (!exists)
            {
                _context.Wishlists.Add(new Wishlist
                {
                    UserId = CurrentUserId,
                    JournalId = journalId,
                    AddedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Added to wishlist.";
            }

            return RedirectToAction("Details", "Journal", new { id = journalId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == CurrentUserId);

            if (item != null)
            {
                _context.Wishlists.Remove(item);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Removed from wishlist.";
            }

            return RedirectToAction("Index");
        }
    }
}