using BookBinding.Data;
using BookBinding.Models;
using BookBinding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookBinding.Controllers
{
    public class CustomJournalController : CustomerBaseController
    {
        private readonly AppDbContext _context;

        public CustomJournalController(AppDbContext context)
        {
            _context = context;
        }

        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        private async Task PopulateOptions(CustomJournalViewModel model)
        {
            model.Leathers = await _context.Leathers.Where(l => l.IsActive)
                .Select(l => new SelectOption { Id = l.Id, Name = l.Type + " - " + l.ColorName }).ToListAsync();

            model.Papers = await _context.Papers.Where(p => p.IsActive)
                .Select(p => new SelectOption { Id = p.Id, Name = p.Name + " (" + p.LengthMm + "x" + p.WidthMm + "mm, " + p.Gsm + "gsm)" })
                .ToListAsync();

            model.BindingStyles = await _context.BindingStyles.Where(b => b.IsActive)
                .Select(b => new SelectOption { Id = b.Id, Name = b.Name }).ToListAsync();
        }

        public async Task<IActionResult> Create()
        {
            var model = new CustomJournalViewModel();
            await PopulateOptions(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomJournalViewModel model)
        {
            bool hasPaperPreset = model.PaperId.HasValue && model.PaperId > 0;
            bool hasCustomDims = model.CustomLengthMm.HasValue && model.CustomWidthMm.HasValue;

            if (!hasPaperPreset && !hasCustomDims)
            {
                ModelState.AddModelError("", "Please either select a preset paper or enter custom Length and Width.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateOptions(model);
                return View(model);
            }

            var request = new CustomJournalRequest
            {
                UserId = CurrentUserId,
                LeatherId = model.LeatherId,
                PaperId = hasPaperPreset ? model.PaperId : null,
                CustomLengthMm = hasPaperPreset ? null : model.CustomLengthMm,
                CustomWidthMm = hasPaperPreset ? null : model.CustomWidthMm,
                DesiredGsm = model.DesiredGsm,
                BindingStyleId = model.BindingStyleId,
                PageCount = model.PageCount,
                EngravingText = model.EngravingText,
                Notes = model.Notes,
                Status = Models.Enums.RequestStatus.Submitted,
                CreatedAt = DateTime.Now
            };

            _context.CustomJournalRequests.Add(request);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your custom journal request has been submitted! We'll review it and follow up with a quote.";
            return RedirectToAction("MyRequests");
        }

        public async Task<IActionResult> MyRequests()
        {
            var requests = await _context.CustomJournalRequests
                .Include(r => r.Leather)
                .Include(r => r.Paper)
                .Include(r => r.BindingStyle)
                .Where(r => r.UserId == CurrentUserId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(requests);
        }

        public async Task<IActionResult> Details(int id)
        {
            var request = await _context.CustomJournalRequests
                .Include(r => r.Leather)
                .Include(r => r.Paper)
                .Include(r => r.BindingStyle)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == CurrentUserId);

            if (request == null) return NotFound();

            return View(request);
        }
    }
}