using DwarfColony.Data;
using DwarfColony.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DwarfColony.Services;

public class SortItems
{
    public IQueryable<Dwarf> Sort(ApplicationDbContext context, string? sortOrder)
    {
        var dwarves = context.
            Dwarves.
            Include(d => d.CurrentArea).
            AsQueryable();  
        
        return sortOrder switch
        {
            "name" => dwarves.OrderBy(d => d.Name),
            "name_desc" => dwarves.OrderByDescending(d => d.Name),
            "age" => dwarves.OrderBy(d => d.Age),
            "age_desc" => dwarves.OrderByDescending(d => d.Age),
            "energy" => dwarves.OrderBy(d => d.Energy),
            "energy_desc" => dwarves.OrderByDescending(d => d.Energy),
            "hunger" => dwarves.OrderBy(d => d.Hunger),
            "hunger_desc" => dwarves.OrderByDescending(d => d.Hunger),
            "thirst" => dwarves.OrderBy(d => d.Thirst),
            "thirst_desc" => dwarves.OrderByDescending(d => d.Thirst),
            "state" => dwarves.OrderBy(d => d.State),
            "state_desc" => dwarves.OrderByDescending(d => d.State),
            "job" => dwarves.OrderBy(d => d.Job),
            "job_desc" => dwarves.OrderByDescending(d => d.Job),
            "status" => dwarves.OrderBy(d => d.Status),
            "status_desc" => dwarves.OrderByDescending(d => d.Status),
            "area" => dwarves.OrderBy(d => d.CurrentArea.Name),
            "area_desc" => dwarves.OrderByDescending(d => d.CurrentArea.Name),
            _ => dwarves.OrderBy(d => d.Name)
        };
    }
}