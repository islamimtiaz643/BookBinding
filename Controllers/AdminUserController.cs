using BookBinding.Data;
using BookBinding.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookBinding.Controllers
{
    public class AdminUserController : AdminBaseController
    {
        private readonly AppDbContext _context;

        public AdminUserController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Where(u => u.Role == UserRole.Customer)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            return View(users);
        }
    }
}