using DwarfColony.Data;
using DwarfColony.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DwarfColony.Services;

public class DwarfRecoveryService(ApplicationDbContext context)
{
    // max value of needs 
    private readonly int _maxValue = 100;

    //food
    private readonly int _needToEat = 75;
    private readonly int _foodRestoreValue = 25;

    // Thirst
    private readonly int _needToDrink = 60;
    private readonly int _thirstRestoreValue = 30;

    // energy
    private readonly int _energyRestoreValue = 12;
    private readonly int _minimumEnergyToSleep = 75;


    public void HandleAutomaticRecovery()
    {
        RestoreThirst();
        RestoreHunger();
    }

    public void RestoreHunger()
    {
        var dwarves = context.Dwarves?.ToList();
        var storages = context.Storages.FirstOrDefault();

        if (dwarves is null || storages is null)
        {
            return;
        }

        foreach (var dwarf in dwarves)
        {
            if (storages.Food >= 1 && dwarf.Hunger < _needToEat)
            {
                dwarf.Hunger = Math.Min(_maxValue, dwarf.Hunger + _foodRestoreValue);
                storages.Food--;
            }
        }
    }

    public void RestoreEnergy(Dwarf? dwarf, int hoursToSleep)
    {
        if (dwarf == null || hoursToSleep <= 0)
        {
            return;
        }

        int restoreValue = _energyRestoreValue * hoursToSleep;

        dwarf.Energy = Math.Min(_maxValue, dwarf.Energy + restoreValue);
    }

    private void RestoreThirst()
    {
        var dwarves = context.Dwarves?.ToList();
        var storages = context.Storages.FirstOrDefault();

        if (dwarves is null || storages is null)
        {
            return;
        }

        foreach (var dwarf in dwarves)
        {
            if (storages.Water >= 1 && dwarf.Thirst <= _needToDrink)
            {
                dwarf.Thirst = Math.Min(_maxValue, dwarf.Thirst + _thirstRestoreValue);
                storages.Water--;
            }
        }
    }
}