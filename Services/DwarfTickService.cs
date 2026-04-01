using DwarfColony.Data;
using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class DwarfTickService
{
    private readonly ApplicationDbContext _context;

    private readonly int _energy = 2;
    private readonly int _hunger = 1;
    private readonly int _thirst = 1;
    private readonly int _jobCost = 4;

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
            var jobCost = dwarf.Job != DwarfJob.None ? _jobCost : 0;

            dwarf.Energy -= _energy + jobCost;
            dwarf.Hunger -= _hunger + jobCost;
            dwarf.Thirst -= _thirst + jobCost;

            dwarf.Energy = Math.Max(0, dwarf.Energy);
            dwarf.Hunger = Math.Max(0, dwarf.Hunger);
            dwarf.Thirst = Math.Max(0, dwarf.Thirst);
        }
    }
}