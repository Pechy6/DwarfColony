using DwarfColony.Data;
using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class SortItems
{
    public IEnumerable<Dwarf> SortByName(ApplicationDbContext context)
    {
        return context.Dwarves.OrderBy(dwarf => dwarf.Name);
    }
    
    public IQueryable<Dwarf> SortByAge(ApplicationDbContext context)
    {
        return context.Dwarves.OrderBy(dwarf => dwarf.Age);
    }
    
    public IQueryable<Dwarf> SortByEnergy(ApplicationDbContext context)
    {
        return context.Dwarves.OrderBy(dwarf => dwarf.Energy);
    }
    
    public IQueryable<Dwarf> SortByHunger(ApplicationDbContext context)
    {
        return context.Dwarves.OrderBy(dwarf => dwarf.Hunger);
    }
    
    public IQueryable<Dwarf> SortByThirst(ApplicationDbContext context)
    {
        return context.Dwarves.OrderBy(dwarf => dwarf.Thirst);
    }
    
    public IQueryable<Dwarf> SortByState(ApplicationDbContext context)
    {
        return context.Dwarves.OrderBy(dwarf => dwarf.State);
    }

    public IQueryable<Dwarf> SortByJob(ApplicationDbContext context)
    {
        return context.Dwarves.OrderBy(dwarf => dwarf.Job);
    }
}