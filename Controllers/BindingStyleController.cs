using BookBinding.Data;
using BookBinding.Models;
using BookBinding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookBinding.Controllers
{
    public class BindingStyleController : AdminBaseController
    {
        private readonly AppDbContext _context;

        public BindingStyleController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var styles = await _context.BindingStyles.OrderByDescending(b => b.Id).ToListAsync();
            return View(styles);
        }

        public IActionResult Create()
        {
            return View(new BindingStyleViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BindingStyleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var style = new BindingStyle
            {
                Name = model.Name,
                Description = model.Description,
                MaxPageCount = model.MaxPageCount,
                ImageUrl = model.ImageUrl,
                IsActive = model.IsActive
            };

            _context.BindingStyles.Add(style);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Binding style created successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var style = await _context.BindingStyles.FindAsync(id);
            if (style == null) return NotFound();

            var model = new BindingStyleViewModel
            {
                Id = style.Id,
                Name = style.Name,
                Description = style.Description,
                MaxPageCount = style.MaxPageCount,
                ImageUrl = style.ImageUrl,
                IsActive = style.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BindingStyleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var style = await _context.BindingStyles.FindAsync(model.Id);
            if (style == null) return NotFound();

            style.Name = model.Name;
            style.Description = model.Description;
            style.MaxPageCount = model.MaxPageCount;
            style.ImageUrl = model.ImageUrl;
            style.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Binding style updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var style = await _context.BindingStyles.FindAsync(id);
            if (style == null) return NotFound();

            _context.BindingStyles.Remove(style);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Binding style deleted.";
            return RedirectToAction("Index");
        }
    }
}