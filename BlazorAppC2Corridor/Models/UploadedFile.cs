namespace BlazorAppC2Corridor.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Extension { get => Path.GetExtension(this.Name); }
        public string FileUrl => $"/api/File/{Id}";
        public UploadedFile()
        {
            this.ModifiedOn = DateTime.Now;
        }
        public string GetUploadedFilePath(bool includeMimeType = false)
        {
            if (includeMimeType)
            {
                return $"/Uploads/{Name}?mime={ContentType}";
            }
            return $"/Uploads/{Name}";
        }


    }
}
