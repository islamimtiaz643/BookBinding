using System.ComponentModel.DataAnnotations;

namespace BookBinding.ViewModels
{
    public class BindingStyleViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Display(Name = "Max Page Count")]
        public int? MaxPageCount { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}