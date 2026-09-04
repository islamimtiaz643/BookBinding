using BookBinding.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBinding.Models
{
    public class Journal
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }

        public string? Description { get; set; }

        public int PaperId { get; set; }
        [ForeignKey("PaperId")]
        public Paper Paper { get; set; }

        public int LeatherId { get; set; }
        [ForeignKey("LeatherId")]
        public Leather Leather { get; set; }

        public int BindingStyleId { get; set; }
        [ForeignKey("BindingStyleId")]
        public BindingStyle BindingStyle { get; set; }

        public int PageCount { get; set; }
        public decimal FinalLengthMm { get; set; }
        public decimal FinalWidthMm { get; set; }
        public decimal ThicknessMm { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public bool IsFeatured { get; set; }
        public JournalStatus Status { get; set; } = JournalStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<JournalImage> Images { get; set; } = new();
    }
}