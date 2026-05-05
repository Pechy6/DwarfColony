using DwarfColony.Data;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.ViewModels;
using DwarfColony.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DwarfColony.Controllers;

public class DwarfOrdersController(
    ApplicationDbContext context,
    TickService tickService,
    SortItems sortItems,
    DwarfMoveToArea moveToArea) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly TickService _tickService = tickService;
    private readonly SortItems _sortItems = sortItems;
    private readonly DwarfMoveToArea _moveToArea = moveToArea;

    // GET
    public IActionResult Index(string? sortOrder)
    {
        var viewModel = new DwarfTableViewModel
        {
            Dwarves = _sortItems.
                Sort(_context, sortOrder).
                ToList(),
            SortOrder = sortOrder
        };
        return View(viewModel);
    }

    public IActionResult SetArea(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dwarf = _context.
            Dwarves.
            Include(d => d.CurrentArea).
            FirstOrDefault(d => d.Id == id);

        var area = _context.Areas.ToList();

        if (dwarf == null)
        {
            return NotFound();
        }

        var viewModel = new SetAreaViewModel
        {
            DwarfId = dwarf.Id,
            DwarfName = dwarf.Name,
            CurrentArea = dwarf.CurrentArea?.Name ?? "Unknow",
            TargetAreas = area.
                Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).
                ToList(),
            Distance = dwarf.TravelRemainingTicks,
            Description = area.FirstOrDefault(a => a.Id == dwarf.TargetAreaId)?.
                Description ?? "No description available"
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetArea(int dwarfId, int targetAreaId, SetAreaPostViewModel viewModel)
    {
        if (dwarfId != viewModel.DwarfId || targetAreaId != viewModel.TargetAreaId)
        {
            return NotFound();
        }

        var dwarf = _context.Dwarves.Find(dwarfId);
        if (dwarf is null)
        {
            return NotFound();
        }

        var targetArea = _context.Areas.Find(targetAreaId);
        if (targetArea is null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _moveToArea.StartTravel(dwarf, targetArea);
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Index));
    }
}