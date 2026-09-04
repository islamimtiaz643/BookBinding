using System.ComponentModel.DataAnnotations;

namespace BookBinding.Models
{
    public class Leather
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Type { get; set; }

        [Required, MaxLength(50)]
        public string ColorName { get; set; }

        public string? ColorHex { get; set; }
        public string? Texture { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}