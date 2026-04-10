using Microsoft.AspNetCore.Mvc;

namespace DwarfColony.Controllers;

public class NeedsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}