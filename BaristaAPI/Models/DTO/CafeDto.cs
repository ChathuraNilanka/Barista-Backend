namespace BaristaAPI.Models.DTO
{
    public class CafeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }
        public string Location { get; set; }
        public ICollection<EmployeeDto>? Employees { get; set; }
    }
}
