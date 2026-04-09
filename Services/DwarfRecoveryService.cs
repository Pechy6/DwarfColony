using DwarfColony.Data;

namespace DwarfColony.Services;

public class DwarfRecoveryService(ApplicationDbContext context)
{
    private readonly int _maxValue = 100;
    private readonly int _foodRestoreValue = 25;

    private void RecoveryHunger()
    {
        var dwarves = context.Dwarves.ToList();
        var storages = context.Storages.FirstOrDefault();

        if (storages is null)
        {
            return;
        }

        foreach (var dwarf in dwarves)
        {
            if (dwarf.Hunger >= _maxValue)
            {
                continue;
            }

            if (storages.Food <= 0)
            {
                break;
            }
            
            var hungerNeeded = _maxValue - dwarf.Hunger;
            var foodUsed = Math.Min(storages.Food, 1);
            var hungerRestored = foodUsed * _foodRestoreValue;

            dwarf.Hunger = Math.Min(_maxValue, dwarf.Hunger + hungerRestored);
            storages.Food -= foodUsed;
        }
    }
}