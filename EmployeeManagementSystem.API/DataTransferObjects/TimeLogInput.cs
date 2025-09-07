namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class TimeLogInput
    {
        public Guid ProjectId { get; set; }
        public int DurationMinutes { get; set; }
    }
}
