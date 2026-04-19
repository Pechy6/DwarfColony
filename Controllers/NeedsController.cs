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
        var dwarves = _context.
            Dwarves.
            Select(dwarf => new DwarfSleepRowViewModel
            {
                Id = dwarf.Id,
                Name = dwarf.Name,
                Job = dwarf.Job.ToString(),
                Hunger = dwarf.Hunger,
                Thirst = dwarf.Thirst,
                Energy = dwarf.Energy,
                CanSleep = _recoveryService.CanSleep(dwarf)
            }).
            ToList();
        
        var storage = _context.Storages.FirstOrDefault();

        if (storage is null)
        {
            return NotFound();
        }

        var model = new NeedsViewModel
        {
            Dwarves = dwarves
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Sleep(NeedsViewModel model)
    {
        if (model.TimeToSleep <= 0 || model.SelectedDwarvesIds == null || !model.SelectedDwarvesIds.Any())
        {
            return View(nameof(Index), model);
        }

        var dwarves = _context.
            Dwarves.
            Where(d => model.SelectedDwarvesIds.Contains(d.Id)).
            ToList();

        foreach (var dwarf in dwarves)
        {
            _recoveryService.RestoreEnergy(dwarf, model.TimeToSleep);
        }

        if (ModelState.IsValid)
        {
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Index));
    }
}