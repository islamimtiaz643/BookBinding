using System.ComponentModel.DataAnnotations;

namespace BookBinding.Models
{
    public class Paper
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        public decimal LengthMm { get; set; }
        public decimal WidthMm { get; set; }
        public int Gsm { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(50)]
        public string DesignType { get; set; }

        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}