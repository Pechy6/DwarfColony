using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class DwarfTickService
{
    private List<Dwarf> Dwarves { get; set; }

    private readonly int _energy = 1;
    private readonly int _hunger = 1;
    private readonly int _thirst = 1;

    public DwarfTickService(List<Dwarf> dwarves)
    {
        Dwarves = dwarves;
    }
    
    public void Tick()
    {
        Stats();
    }

    private void Stats()
    {
        foreach (var dwarf in Dwarves)
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