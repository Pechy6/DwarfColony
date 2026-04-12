using DwarfColony.Data;
using DwarfColony.Services;
using Microsoft.AspNetCore.Mvc;

namespace DwarfColony.Controllers;

public class NeedsController(ApplicationDbContext context, DwarfRecoveryService recoveryService) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly DwarfRecoveryService _recoveryService = recoveryService;

    // GET
    public IActionResult Index()
    {
        var dwarves =  _context.Dwarves.ToList();
        return View(dwarves);
    }
}