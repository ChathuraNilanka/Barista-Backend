namespace BaristaAPI.Models.DTO
{
    public class UpdateCafeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }
        public string Location { get; set; }
        public ICollection<AddEmployeeDto>? Employees { get; set; }
    }
}
