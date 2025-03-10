using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    
    [BindProperty]
    public LoginInputModel Input { get; set; }

    public class LoginInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Authenticate user (Add your login logic here)
        if (Input.Email == "admin@admin.com" && Input.Password == "pwd")
        {
            return RedirectToPage("/Views/Chatbot/Chatbot");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}