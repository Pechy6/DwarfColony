using DwarfColony.Data;
using DwarfColony.Models.Entities;
using DwarfColony.Models.ViewModels;
using DwarfColony.Services;
using Microsoft.AspNetCore.Mvc;

namespace DwarfColony.Controllers;

public class DwarvesController(ApplicationDbContext context, DwarfFactory dwarfFactory) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly DwarfFactory _dwarfFactory = dwarfFactory;

    // GET
    public IActionResult Index()
    {
        var dwarves = _context.Dwarves.ToList();
        return View(dwarves);
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
        if (ModelState.IsValid)
        {
            Dwarf dwarf = _dwarfFactory.Create(createDwarfModel);
            _context.Dwarves.Add(dwarf);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(createDwarfModel);
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

        var editDwarfModel = new EditDwarfModel
        {
            Id = dwarfEntity.Id,
            Name = dwarfEntity.Name
        };
        
        return View(editDwarfModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, EditDwarfModel editDwarfModel)
    {
        if (id != editDwarfModel.Id)
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
            dwarfEntity.Name = editDwarfModel.Name;
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(editDwarfModel);
    }
}