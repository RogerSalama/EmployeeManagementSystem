namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class CheckoutLogReturn
    {
        public int ProjectID {  get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
