namespace Bloggie.Web.Repositories
{
    public interface IImageRepository
    {
        Task<string>uploadAsync(IFormFile file);
    }
}
