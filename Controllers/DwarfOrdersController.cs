using DwarfColony.Data;
using DwarfColony.Models.ViewModels;
using DwarfColony.Services;
using Microsoft.AspNetCore.Mvc;

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
}