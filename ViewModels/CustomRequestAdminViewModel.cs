using BookBinding.Models.Enums;

namespace BookBinding.ViewModels
{
    public class CustomRequestAdminViewModel
    {
        public int Id { get; set; }
        public RequestStatus Status { get; set; }
        public string? AdminNote { get; set; }
    }
}