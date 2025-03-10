using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Models;

public class ChatbotModel : PageModel
{
    public string ChatHistory { get; set; } = "";

    public void OnGet()
    {
        ChatHistory = "<p>Welcome to Kacrut AI!</p>";
    }
}