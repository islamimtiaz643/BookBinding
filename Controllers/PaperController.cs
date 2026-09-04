using BookBinding.Data;
using BookBinding.Models;
using BookBinding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookBinding.Controllers
{
    public class PaperController : AdminBaseController
    {
        private readonly AppDbContext _context;

        public PaperController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var papers = await _context.Papers.OrderByDescending(p => p.Id).ToListAsync();
            return View(papers);
        }

        public IActionResult Create()
        {
            return View(new PaperViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaperViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var paper = new Paper
            {
                Name = model.Name,
                LengthMm = model.LengthMm,
                WidthMm = model.WidthMm,
                Gsm = model.Gsm,
                Color = model.Color,
                DesignType = model.DesignType,
                ImageUrl = model.ImageUrl,
                IsActive = model.IsActive
            };

            _context.Papers.Add(paper);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Paper created successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var paper = await _context.Papers.FindAsync(id);
            if (paper == null) return NotFound();

            var model = new PaperViewModel
            {
                Id = paper.Id,
                Name = paper.Name,
                LengthMm = paper.LengthMm,
                WidthMm = paper.WidthMm,
                Gsm = paper.Gsm,
                Color = paper.Color,
                DesignType = paper.DesignType,
                ImageUrl = paper.ImageUrl,
                IsActive = paper.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaperViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var paper = await _context.Papers.FindAsync(model.Id);
            if (paper == null) return NotFound();

            paper.Name = model.Name;
            paper.LengthMm = model.LengthMm;
            paper.WidthMm = model.WidthMm;
            paper.Gsm = model.Gsm;
            paper.Color = model.Color;
            paper.DesignType = model.DesignType;
            paper.ImageUrl = model.ImageUrl;
            paper.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Paper updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var paper = await _context.Papers.FindAsync(id);
            if (paper == null) return NotFound();

            _context.Papers.Remove(paper);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Paper deleted.";
            return RedirectToAction("Index");
        }
    }
}