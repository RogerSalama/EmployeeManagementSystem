public class ViewEmployeeDTO
{
    public int EmployeeID { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Military_Status { get; set; }
    public string ManagerName { get; set; }
    public string VacationLevelInfo { get; set; }  // string with Type1/Type2 days
    public List<string> Projects { get; set; }
}
