using System.ComponentModel.DataAnnotations.Schema;

namespace BaristaAPI.Models.DTO
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public Guid CafeId { get; set; }
    }
}
