using DwarfColony.Data;
using DwarfColony.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DwarfColony.Services;

public class DwarfRecoveryService(ApplicationDbContext context)
{
    // max value of needs 
    private readonly int _maxValue = 100;
    
    //food
    private readonly int _foodRestoreValue = 25;

    // Thirst
    private readonly int _needToDrink = 60;
    private readonly int _thirstRestoreValue = 40;
    
    // energy
    private readonly int _energyRestoreValue = 12;


    public void RestoreHunger(Dwarf? dwarf, int countOfFoods)
    {
        if (dwarf == null || countOfFoods <= 0)
        {
            return;
        }

        var storages = context.Storages.FirstOrDefault();

        if (storages is null)
        {
            return;
        }
        
        int restoreValue = _foodRestoreValue * countOfFoods;

        if (countOfFoods <= storages.Food)
        {
            dwarf.Hunger = Math.Min(_maxValue, dwarf.Hunger + restoreValue);
            storages.Food -= countOfFoods;
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

    public void HandleAutomaticRecovery()
    {
        RestoreThirst();
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