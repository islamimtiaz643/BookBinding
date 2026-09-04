using System.ComponentModel.DataAnnotations.Schema;

namespace BookBinding.Models
{
    public class JournalImage
    {
        public int Id { get; set; }

        public int JournalId { get; set; }
        [ForeignKey("JournalId")]
        public Journal Journal { get; set; }

        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
    }
}