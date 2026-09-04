using System.ComponentModel.DataAnnotations;

namespace BookBinding.Models.ViewModels
{
    public class PaperViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required, Range(1, 5000)]
        [Display(Name = "Length (mm)")]
        public decimal LengthMm { get; set; }

        [Required, Range(1, 5000)]
        [Display(Name = "Width (mm)")]
        public decimal WidthMm { get; set; }

        [Required, Range(1, 1000)]
        public int Gsm { get; set; }

        [Required, MaxLength(50)]
        public string Color { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Design Type")]
        public string DesignType { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}