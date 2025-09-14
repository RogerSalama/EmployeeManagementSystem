public class ViewProjectDTO
{
    public int ProjectID { get; set; }
    public string ProjectName { get; set; }
    public List<ViewEmployeesinProjectDto> Employees { get; set; }
    public string CustomerName { get; set; }

}

public class ViewEmployeesinProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }

}
