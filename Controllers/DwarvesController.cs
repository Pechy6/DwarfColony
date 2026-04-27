using DwarfColony.Data;
using DwarfColony.Models.Entities;
using Microsoft.EntityFrameworkCore;
using DwarfColony.Models.ViewModels;
using DwarfColony.Services;
using Microsoft.AspNetCore.Mvc;

namespace DwarfColony.Controllers;

public class DwarvesController(
    ApplicationDbContext context,
    DwarfFactory dwarfFactory,
    TickService tickService,
    DwarfStateService dwarfStateService,
    SortItems sortItems) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly DwarfFactory _dwarfFactory = dwarfFactory;
    private readonly TickService _tickService = tickService;
    private readonly DwarfStateService _dwarfStateService = dwarfStateService;
    private readonly SortItems _sortItems = sortItems;
    

    // GET
    public IActionResult Index(string? sortOrder)
    {
        var indexViewModel = new IndexViewModel
        {
            Dwarves = _sortItems.Sort(_context, sortOrder).ToList(),
            SortOrder = sortOrder
        };
        return View(indexViewModel);
    }

    public IActionResult Create()
    {
        CreateDwarfModel dwarfModel = new();
        return View(dwarfModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateDwarfModel createDwarfModel)
    {
        var startingAreaId = _context.Areas.First();
        if (ModelState.IsValid)
        {
            var dwarf = _dwarfFactory.Create(createDwarfModel, startingAreaId.Id);
            _context.Dwarves.Add(dwarf);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(createDwarfModel);
    }

    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dwarfEntity = _context.Dwarves.Find(id);

        if (dwarfEntity == null)
        {
            return NotFound();
        }

        return View(dwarfEntity);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dwarfEntity = _context.Dwarves.Find(id);

        if (dwarfEntity == null)
        {
            return NotFound();
        }

        var editDwarfModel = new EditDwarfJobModel
        {
            Id = dwarfEntity.Id,
            Name = dwarfEntity.Name,
            Job = dwarfEntity.Job
        };

        return View(editDwarfModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, EditDwarfJobModel editDwarfJobModel)
    {
        if (id != editDwarfJobModel.Id)
        {
            return NotFound();
        }

        var dwarfEntity = _context.Dwarves.Find(id);
        if (dwarfEntity == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            dwarfEntity.Job = editDwarfJobModel.Job;
            _dwarfStateService.ChangeState(dwarfEntity);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(editDwarfJobModel);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dwarfEntity = _context.Dwarves.Find(id);

        if (dwarfEntity == null)
        {
            return NotFound();
        }

        return View(dwarfEntity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var dwarfEntity = _context.Dwarves.Find(id);

        if (dwarfEntity != null)
        {
            _context.Dwarves.Remove(dwarfEntity);
        }

        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Tick()
    {
        _tickService.Tick();
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}