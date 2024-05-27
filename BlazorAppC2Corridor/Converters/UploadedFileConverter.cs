using Microsoft.AspNetCore.Components.Forms;
using BlazorAppC2Corridor.Models;

namespace BlazorAppC2Corridor.Converters
{
    public static class UploadedFileConverter
    {
        public static async Task<UploadedFile> ConvertToUploadedFileAsync(this IBrowserFile formFile)
        {
            UploadedFile? file = null;
            using (var ms = new MemoryStream())
            {
                var fileStream = formFile.OpenReadStream(52428800); //max file size that can be opened (50MB)
                await fileStream.CopyToAsync(ms);
                file = new UploadedFile()
                {
                    Data = ms.ToArray(),
                    ContentType = formFile.ContentType,
                    Name = formFile.Name
                };
            }
            return file;
        }
        public static string? ConvertToBrowserRenderableFile(this UploadedFile file)
        {
            if (file != null)
            {
                string base64Data = Convert.ToBase64String(file.Data);
                string dataUrl;

                if (file.ContentType.StartsWith("image"))
                {
                    dataUrl = $"data:{file.ContentType};base64,{base64Data}";
                }
                else if (file.ContentType.StartsWith("video"))
                {
                    dataUrl = $"data:{file.ContentType};base64,{base64Data}";
                }
                else
                {
                    return null;
                }

                return dataUrl;
            }
            return null;
        }

    }
}
