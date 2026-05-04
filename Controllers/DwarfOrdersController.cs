using DwarfColony.Data;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.ViewModels;
using DwarfColony.Services;
using Microsoft.AspNetCore.Mvc;
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
        
        if (dwarf == null)
        {
            return NotFound();
        }

        var viewModel = new SetAreaViewModel
        {
            DwarfName = dwarf.Name,
            CurrentArea = dwarf.CurrentArea?.Name ?? "Unknow",
            TargetAreas = _context.Areas.ToList(),
            Distance = dwarf.TravelRemainingTicks
        };
        return View(viewModel);
    }
}