using System.ComponentModel.DataAnnotations;

namespace BookBinding.ViewModels
{
    public class CustomJournalViewModel
    {
        [Required, Display(Name = "Leather")]
        public int LeatherId { get; set; }

        [Display(Name = "Paper (choose a preset, or leave blank for fully custom)")]
        public int? PaperId { get; set; }

        [Display(Name = "Custom Length (mm)")]
        public decimal? CustomLengthMm { get; set; }

        [Display(Name = "Custom Width (mm)")]
        public decimal? CustomWidthMm { get; set; }

        [Display(Name = "Desired GSM")]
        public int? DesiredGsm { get; set; }

        [Required, Display(Name = "Binding Style")]
        public int BindingStyleId { get; set; }

        [Required, Range(1, 2000)]
        [Display(Name = "Page Count")]
        public int PageCount { get; set; } = 100;

        [MaxLength(100)]
        [Display(Name = "Engraving Text (optional)")]
        public string? EngravingText { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        // For populating the pickers
        public List<SelectOption> Leathers { get; set; } = new();
        public List<SelectOption> Papers { get; set; } = new();
        public List<SelectOption> BindingStyles { get; set; } = new();
    }
}