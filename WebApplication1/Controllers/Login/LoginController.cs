using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.Login;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}