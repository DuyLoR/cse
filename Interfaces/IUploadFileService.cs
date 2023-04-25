namespace CSE_website.Interfaces
{
    public interface IUploadFileService 
    {
        Task<string> LoadFile(IFormFile? file, string folder = "uploads");
        Task<string> LoadMultipleFile(IFormFile[] files, string folder = "uploads");
    }
}