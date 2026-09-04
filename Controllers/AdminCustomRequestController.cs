using BookBinding.Data;
using BookBinding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookBinding.Controllers
{
    public class AdminCustomRequestController : AdminBaseController
    {
        private readonly AppDbContext _context;

        public AdminCustomRequestController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _context.CustomJournalRequests
                .Include(r => r.User)
                .Include(r => r.Leather)
                .Include(r => r.Paper)
                .Include(r => r.BindingStyle)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(requests);
        }

        public async Task<IActionResult> Details(int id)
        {
            var request = await _context.CustomJournalRequests
                .Include(r => r.User)
                .Include(r => r.Leather)
                .Include(r => r.Paper)
                .Include(r => r.BindingStyle)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null) return NotFound();

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(CustomRequestAdminViewModel model)
        {
            var request = await _context.CustomJournalRequests.FindAsync(model.Id);
            if (request == null) return NotFound();

            request.Status = model.Status;
            request.AdminNote = model.AdminNote;
            request.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Request updated.";
            return RedirectToAction("Details", new { id = model.Id });
        }
    }
}