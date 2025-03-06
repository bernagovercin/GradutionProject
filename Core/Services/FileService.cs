using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // Dosya kaydetme işlemi
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            // wwwroot/uploads/products dizininde dosyaları saklamak için yol
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "products");

            // Dizin yoksa oluştur
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder); // Klasörü oluştur
            }

            // Dosyanın kaydedileceği tam yol
            var filePath = Path.Combine(uploadsFolder, file.FileName);

            // Dosyayı kaydetme işlemi
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Görselin yolunu döndür
            return Path.Combine("uploads", "products", file.FileName); // Veritabanına kaydedilecek yol
        }
    }
}
