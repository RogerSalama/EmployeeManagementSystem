namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class UpdateSessionTimestampRequest
    {
        public int SessionId { get; set; }
        public DateTimeOffset NewTimestamp { get; set; }
    }
}