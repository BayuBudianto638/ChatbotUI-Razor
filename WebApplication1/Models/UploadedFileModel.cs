namespace WebApplication1.Models;

public class UploadedFileModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FileName { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public string createdBy { get; set; }
    public string WeaviateObjectId { get; set; } 
}