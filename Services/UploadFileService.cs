using Microsoft.Win32;
using CSE_website.Interfaces;
namespace CSE_website.Services;

/// <summary>
/// Service for uploading file
/// </summary>
public class UploadFileService : BaseService, IUploadFileService
{
    public UploadFileService(IWebHostEnvironment environment)
        : base(environment) { }

    /// <summary>
    /// Upload file
    /// </summary>
    /// <param name="file">File upload</param>
    /// <param name="folder">Folder from wwwroot to upload</param>
    /// <returns>path</returns>
    public async Task<string> LoadFile(IFormFile? file, string folder = "uploads")
    {
        if (file != null)
        {
            var relativePath = Path.Combine(folder, file.GetHashCode() + file.FileName);
            var filePath = Path.Combine(Environment.WebRootPath, relativePath);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return relativePath;
        }

        return null;
    }
    public byte[] GetFileBytes(string Outline)
    {
        byte[] fileBytes = null;

        if (File.Exists(Outline))
        {
            fileBytes = File.ReadAllBytes(Outline);
        }

        return fileBytes;
    }
    public string GetContentType(string Outline)
    {
        string contentType = "application/octet-stream";
        string fileExtension = Path.GetExtension(Outline);

        if (!string.IsNullOrEmpty(fileExtension))
        {
            // Remove the leading dot (.) from the file extension
            fileExtension = fileExtension.Substring(1);

            if (!string.IsNullOrEmpty(fileExtension))
            {
                // Try to get the MIME type from the registry
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(fileExtension);

                if (key != null && key.GetValue("Content Type") != null)
                {
                    contentType = key.GetValue("Content Type").ToString();
                }
            }
        }

        return contentType;
    }
    /// <summary>
    /// Get the file name from the file path
    /// </summary>
    /// <param name="filePath">The file path</param>
    /// <returns>The file name</returns>
    public string GetFileName(string Outline)
    {
        return Path.GetFileName(Outline);
    }

    /// <summary>
    /// Upload multiple files
    /// </summary>
    /// <param name="files">Files upload</param>
    /// <param name="folder">Folder from wwwroot to upload</param>
    /// <returns>path</returns>
    public async Task<string> LoadMultipleFile(IFormFile[] files, string folder = "uploads")
    {
        if (files.Length != 0)
        {
            foreach (var file in files)
            {
                var relativePath = Path.Combine(folder, file.GetHashCode() + file.FileName);
                var filePath = Path.Combine(Environment.WebRootPath, "uploads", file.GetHashCode() + file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return relativePath;
            }
        }

        return null;
    }
}