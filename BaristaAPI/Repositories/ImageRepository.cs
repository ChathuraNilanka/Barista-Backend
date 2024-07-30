

using BaristaAPI.Data;
using BaristaAPI.Models.Domain;
using BaristaAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly APIDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor, APIDbContext dbContext)
        {
            this.webHost = webHost;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHost.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);

            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            var existCafe = await dbContext.Cafes.FirstOrDefaultAsync(x => x.Id == image.CafeId);
            if (existCafe == null)
            {
                return null;
            }

            existCafe.Logo = image.FilePath;

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }

    }
}
