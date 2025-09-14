public class VacationDto
{
    public int VacationID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty; // e.g., Pending, Approved, Rejected
}
