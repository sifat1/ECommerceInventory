namespace ECommerceInventory.Services;
public class FileUploadService
{
    private readonly IWebHostEnvironment _env;

    public FileUploadService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;
        
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var uploads = Path.Combine(_env.WebRootPath, "uploads");

        if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);

        var filePath = Path.Combine(uploads, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        
        return $"/uploads/{fileName}";
    }
}
