namespace WebApplication1.Models;

public class ChatHistoryModel
{
    public string Id { get; set; }
    public string SessionId { get; set; } 
    public string User { get; set; } 
    public string Message { get; set; } 
    public DateTime Timestamp { get; set; } 
    public string Role { get; set; } 
}