using System.ComponentModel.DataAnnotations;

namespace BookBinding.ViewModels
{
    public class LeatherViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Type { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Color Name")]
        public string ColorName { get; set; }

        [Display(Name = "Color Hex")]
        public string? ColorHex { get; set; }

        public string? Texture { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}