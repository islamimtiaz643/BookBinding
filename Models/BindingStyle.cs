using System.ComponentModel.DataAnnotations;

namespace BookBinding.Models
{
    public class BindingStyle
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }
        public int? MaxPageCount { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}