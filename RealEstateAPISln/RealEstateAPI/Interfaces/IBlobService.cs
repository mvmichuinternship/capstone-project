namespace RealEstateAPI.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadFileAsync(IFormFile imageFile);
        Task<string> GetFileUrlAsync(string imageName);
    }
}
