using DwarfColony.Data;
using DwarfColony.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DwarfColony.ViewComponents;

public class NavbarStatsViewComponent(ApplicationDbContext context) : ViewComponent
{
    private readonly ApplicationDbContext _context = context;

    public IViewComponentResult Invoke()
    {
        var storage = _context.Storages.FirstOrDefault();
        var dwarves = _context.Dwarves.Count();
        
        var model = new ResourceViewModel
        {
            Water = storage?.Water ?? 0,
            Food = (storage?.Food ?? 0) + (storage?.RawFood ?? 0),
            Wood = storage?.Wood ?? 0,
            Materials = (storage?.Stone ?? 0) + (storage?.IronCore ?? 0) + (storage?.Coal ?? 0),
            Dwarves = dwarves
        };
        return View(model);
    }
}