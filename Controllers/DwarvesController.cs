using DwarfColony.Data;
using DwarfColony.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DwarfColony.Controllers;

public class DwarvesController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    // GET
    public IActionResult Index()
    {
        var dwarves = _context.Dwarves.ToList();
        return View(dwarves);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Dwarf dwarf)
    {
        if (ModelState.IsValid)
        {
            _context.Dwarves.Add(dwarf);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(dwarf);
    }
}