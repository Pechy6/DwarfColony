using DwarfColony.Data;
using DwarfColony.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DwarfColony.ViewComponents;

public class NavbarStatsViewComponent(ApplicationDbContext context) : ViewComponent
{
    private readonly ApplicationDbContext _context = context;

    /// <summary>
    /// Retrieves the current state of resources and the number of dwarves
    /// from the database, aggregates the relevant information, and renders a view
    /// with a model containing these details.
    /// </summary>
    /// <returns>An instance of <see cref="IViewComponentResult"/> containing
    /// the aggregated resource data and dwarf count for display in the view.</returns>
    public IViewComponentResult Invoke()
    {
        var storage = _context.Storages.FirstOrDefault();
        var dwarves = _context.Dwarves.Count();
        
        var model = new ResourceViewModel
        {
            Water = storage?.Water ?? 0,
            Food = (storage?.Food ?? 0),
            RawFood = storage?.RawFood ?? 0,
            Wood = storage?.Wood ?? 0,
            Materials = storage?.Stone ?? 0,
            Dwarves = dwarves
        };
        return View(model);
    }
}