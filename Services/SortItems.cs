using DwarfColony.Data;
using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class SortItems
{
    public IQueryable<Dwarf> Sort(ApplicationDbContext context, string? sortOrder)
    {
        return sortOrder switch
        {
            "name" => context.Dwarves.OrderBy(d => d.Name),
            "name_desc" => context.Dwarves.OrderByDescending(d => d.Name),
            "age" => context.Dwarves.OrderBy(d => d.Age),
            "age_desc" => context.Dwarves.OrderByDescending(d => d.Age),
            "energy" => context.Dwarves.OrderBy(d => d.Energy),
            "energy_desc" => context.Dwarves.OrderByDescending(d => d.Energy),
            "hunger" => context.Dwarves.OrderBy(d => d.Hunger),
            "hunger_desc" => context.Dwarves.OrderByDescending(d => d.Hunger),
            "thirst" => context.Dwarves.OrderBy(d => d.Thirst),
            "thirst_desc" => context.Dwarves.OrderByDescending(d => d.Thirst),
            "state" => context.Dwarves.OrderBy(d => d.State),
            "state_desc" => context.Dwarves.OrderByDescending(d => d.State),
            "job" => context.Dwarves.OrderBy(d => d.Job),
            "job_desc" => context.Dwarves.OrderByDescending(d => d.Job),
            "status" => context.Dwarves.OrderBy(d => d.Status),
            "status_desc" => context.Dwarves.OrderByDescending(d => d.Status),
            _ => context.Dwarves.OrderBy(d => d.Name)
        };
    }
}