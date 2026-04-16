using DwarfColony.Data;
using DwarfColony.Models.ViewModels;
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
        var storage = _context.Storages.FirstOrDefault();

        if (storage is null)
        {
            return NotFound();
        }

        var model = new NeedsViewModel
        {
            Dwarves = dwarves,
        };
        
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Feed(NeedsViewModel model)
    {
        var dwarf = _context.Dwarves.Find(model.Id);
        if (dwarf == null)
        {
            return NotFound();
        }
        
        if (ModelState.IsValid)
        {
            _recoveryService.RestoreHunger(dwarf, model.FoodToUse);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}