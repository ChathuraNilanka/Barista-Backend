

using BaristaAPI.Models.Domain;

namespace BaristaAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
