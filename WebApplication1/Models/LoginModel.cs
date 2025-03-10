using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Models;

public class LoginModel : PageModel
{
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

        if (Input.Email == "admin@admin.com" && Input.Password == "pwd")
        {
            return RedirectToPage("/index");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}