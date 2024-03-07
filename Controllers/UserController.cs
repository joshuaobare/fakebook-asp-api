using Microsoft.AspNetCore.Mvc;
using fakebook_asp_api.Models;
using fakebook_asp_api.Services;


namespace fakebook_asp_api.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
