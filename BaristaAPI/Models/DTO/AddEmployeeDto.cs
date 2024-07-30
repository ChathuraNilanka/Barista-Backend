namespace BaristaAPI.Models.DTO
{
    public class AddEmployeeDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? CafeId { get; set; }
    }
}
