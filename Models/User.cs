using BookBinding.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookBinding.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }

        public UserRole Role { get; set; } = UserRole.Customer;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}