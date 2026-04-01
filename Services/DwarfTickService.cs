using DwarfColony.Data;
using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class DwarfTickService
{
    private readonly ApplicationDbContext _context;

    private readonly int _energy = 1;
    private readonly int _hunger = 1;
    private readonly int _thirst = 1;

    public DwarfTickService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public void Tick()
    {
        Stats();
    }

    private void Stats()
    {
        var dwarves = _context.Dwarves.ToList();
        
        foreach (var dwarf in dwarves)
        {
            dwarf.Energy -= _energy;
            if (dwarf.Energy <= 0)
            {
                dwarf.Energy = 0;
            }

            dwarf.Hunger -= _hunger;
            if (dwarf.Hunger <= 0)
            {
                dwarf.Hunger = 0;
            }

            dwarf.Thirst -= _thirst;
            if (dwarf.Thirst <= 0)
            {
                dwarf.Thirst = 0;
            }
        }
    }
}