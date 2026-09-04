using BookBinding.Data;
using BookBinding.Models;
using BookBinding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookBinding.Controllers
{
    public class LeatherController : AdminBaseController
    {
        private readonly AppDbContext _context;

        public LeatherController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var leathers = await _context.Leathers.OrderByDescending(l => l.Id).ToListAsync();
            return View(leathers);
        }

        public IActionResult Create()
        {
            return View(new LeatherViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeatherViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var leather = new Leather
            {
                Type = model.Type,
                ColorName = model.ColorName,
                ColorHex = model.ColorHex,
                Texture = model.Texture,
                ImageUrl = model.ImageUrl,
                IsActive = model.IsActive
            };

            _context.Leathers.Add(leather);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Leather created successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leather = await _context.Leathers.FindAsync(id);
            if (leather == null) return NotFound();

            var model = new LeatherViewModel
            {
                Id = leather.Id,
                Type = leather.Type,
                ColorName = leather.ColorName,
                ColorHex = leather.ColorHex,
                Texture = leather.Texture,
                ImageUrl = leather.ImageUrl,
                IsActive = leather.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeatherViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var leather = await _context.Leathers.FindAsync(model.Id);
            if (leather == null) return NotFound();

            leather.Type = model.Type;
            leather.ColorName = model.ColorName;
            leather.ColorHex = model.ColorHex;
            leather.Texture = model.Texture;
            leather.ImageUrl = model.ImageUrl;
            leather.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Leather updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var leather = await _context.Leathers.FindAsync(id);
            if (leather == null) return NotFound();

            _context.Leathers.Remove(leather);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Leather deleted.";
            return RedirectToAction("Index");
        }
    }
}