namespace WebApplication1.Models;

public class ChatSessionModel
{
    public string Id { get; set; }

    public string UserId { get; set; } 
    public DateTime StartTime { get; set; } 
    public DateTime? EndTime { get; set; } 
    public string Status { get; set; } 
}