namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class VacationRequestInput
    {
        public int VacationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
    }
}
