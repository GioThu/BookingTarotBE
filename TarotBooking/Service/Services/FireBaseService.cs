using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class FirebaseService
{
    private readonly string _bucket;
    public FirebaseService(IConfiguration configuration)
    {
        _bucket = configuration["Firebase:StorageBucket"];
    }

    public async Task<string> GetImageUrlAsync(string fileName)
    {
        //var auth = new FirebaseAuthProvider(new FirebaseConfig("your-api-key"));
        var storage = new FirebaseStorage(_bucket);

        var imageUrl = await storage.Child("images").Child(fileName).GetDownloadUrlAsync();
        return imageUrl;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCYs-f-LSG-8gLHNCboO8aaEf6D2Gur68k"));
        var storage = new FirebaseStorage(_bucket);

        // Đặt tên cho file (có thể là unique ID hoặc thời gian)
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        using (var stream = file.OpenReadStream())
        {
            // Tải file lên Firebase Storage
            var uploadTask = await storage
                .Child("images")
                .Child(fileName)
                .PutAsync(stream);

            // Trả về URL của file đã tải lên
            return await storage.Child("images").Child(fileName).GetDownloadUrlAsync();
        }
    }

    public async Task DeleteImageAsync(string imageUrl)
    {
        var storage = new FirebaseStorage(_bucket);

        // Lấy tên file từ URL
        var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);

        // Xóa file khỏi Firebase Storage
        await storage.Child("images").Child(fileName).DeleteAsync();
    }
}
