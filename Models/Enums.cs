namespace BookBinding.Models.Enums
{
    public enum UserRole
    {
        Admin,
        Customer
    }

    public enum RequestStatus
    {
        Submitted,
        Reviewed,
        Quoted,
        InProgress,
        Completed,
        Cancelled
    }

    public enum JournalStatus
    {
        Draft,
        Active,
        SoldOut
    }
}