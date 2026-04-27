using DwarfColony.Data;
using DwarfColony.Models.Entities;
using DwarfColony.Models.Entities.Dwarfs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DwarfColony.Services;

public class DwarfRecoveryService(ApplicationDbContext context, DwarfStateService dwarfStateService)
{
    // max value of needs 
    private readonly int _maxValue = 100;

    //food
    private readonly int _needToEat = 75;
    private readonly int _foodRestoreValue = 25;

    // Thirst
    private readonly int _needToDrink = 60;
    private readonly int _thirstRestoreValue = 30;

    // energy (sleep)
    private readonly int _energyRestoreValue = 12;
    private readonly int _minimumEnergyToSleep = 75;


    public void HandleAutomaticRecovery()
    {
        RestoreThirst();
        RestoreHunger();
    }

    public void HandleSleepTick()
    {
        ProcessSleepTick();
    }

    //Automatic recovery (hunger, thirst)

    /// <summary>
    /// Obnoví hlad trpaslíků pomocí dostupných zásob jídla ve skladu.
    /// </summary>
    private void RestoreHunger()
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

    /// <summary>
    /// Obnoví žízeň trpaslíků pomocí dostupných zásob vody ve skladu.
    /// </summary>
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

    // Handle sleep

    public void SetSleep(Dwarf? dwarf, int hoursToSleep)
    {
        if (dwarf == null || hoursToSleep <= 0)
        {
            return;
        }

        if (CanSleep(dwarf))
        {
            if (dwarf.State != DwarfState.Sleeping)
            {
                dwarf.State = DwarfState.Sleeping;
                dwarf.ActionRemainingTime = hoursToSleep;
            }
        }
    }

    private void ProcessSleepTick()
    {
        var dwarves = context.Dwarves?.ToList();

        if (dwarves is null)
        {
            return;
        }

        var sleepingDwarves = dwarves.Where(d => d.State == DwarfState.Sleeping);

        foreach (var dwarf in sleepingDwarves)
        {
            if (dwarf.ActionRemainingTime > 0)
            {
                dwarf.Energy = Math.Min(_maxValue, dwarf.Energy + _energyRestoreValue);
                dwarf.ActionRemainingTime--;
            }


            if (dwarf.ActionRemainingTime <= 0 || dwarf.Energy >= _maxValue)
            {
                dwarfStateService.RestoreStateAfterSleeping(dwarf);
                dwarf.ActionRemainingTime = 0;
            }
        }
    }


    /// <summary>
    /// Vrací, zda může trpaslík spát podle aktuální úrovně energie.
    /// </summary>
    /// <param name="dwarf">Trpaslík, který se má vyhodnotit. Nesmí být <c>null</c>.</param>
    /// <returns><c>true</c>, pokud je energie trpaslíka nižší než minimální hodnota pro spánek; jinak <c>false</c>.</returns>
    public bool CanSleep(Dwarf? dwarf)
    {
        if (dwarf == null)
        {
            return false;
        }

        return dwarf.Energy < _minimumEnergyToSleep;
    }
}