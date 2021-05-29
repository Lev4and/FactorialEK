using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace FactorialEK.AspNetCore.Service
{
    public class UploadFileService
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public UploadFileService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<bool> UploadFileAsync(IFormFile uploadedFile, string savePath)
        {
            if (uploadedFile != null)
            {
                if (!string.IsNullOrEmpty(uploadedFile.FileName))
                {
                    savePath += uploadedFile.FileName;

                    if (!File.Exists(_appEnvironment.WebRootPath + "/" + savePath))
                    {
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/" + savePath, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);

                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
