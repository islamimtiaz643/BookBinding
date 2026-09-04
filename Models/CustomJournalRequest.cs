using BookBinding.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBinding.Models
{
    public class CustomJournalRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int LeatherId { get; set; }
        [ForeignKey("LeatherId")]
        public Leather Leather { get; set; }

        public int? PaperId { get; set; }
        [ForeignKey("PaperId")]
        public Paper? Paper { get; set; }

        public decimal? CustomLengthMm { get; set; }
        public decimal? CustomWidthMm { get; set; }
        public int? DesiredGsm { get; set; }

        public int BindingStyleId { get; set; }
        [ForeignKey("BindingStyleId")]
        public BindingStyle BindingStyle { get; set; }

        public int PageCount { get; set; }
        public string? EngravingText { get; set; }
        public string? Notes { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Submitted;
        public string? AdminNote { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}