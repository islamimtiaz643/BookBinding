using System.ComponentModel.DataAnnotations.Schema;

namespace BookBinding.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int JournalId { get; set; }
        [ForeignKey("JournalId")]
        public Journal Journal { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}