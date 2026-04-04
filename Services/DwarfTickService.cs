using DwarfColony.Data;
using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class DwarfTickService
{
    private readonly ApplicationDbContext _context;

    // Constants for dwarf stats
    private readonly int _energy = 2;
    private readonly int _hunger = 1;
    private readonly int _thirst = 1;
    private readonly int _jobCost = 4;

    // Thresholds for determining dwarf status
    private const int FitThreshold = 51;
    private const int StrainThreshold = 25;

    public DwarfTickService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Tick()
    {
        Stats();
        StatusControl();
    }

    /// <summary>
    /// Updates the energy, hunger, and thirst values for all dwarves in the colony.
    /// </summary>
    /// <remarks>
    /// Each dwarf loses a fixed amount of energy, hunger, and thirst during a tick.
    /// If the dwarf has a job assigned, the job cost is added to each stat decrease.
    /// The values are then clamped so they do not drop below zero.
    /// </remarks>
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

    /// <summary>
    /// Updates the status of all dwarves in the colony based on their current energy, hunger, and thirst levels.
    /// </summary>
    /// <remarks>
    /// The status is determined using predefined thresholds for energy, hunger, and thirst:
    /// - If all attributes are at or above the Fit threshold, the status is set to Fit.
    /// - If all attributes are at or above the Strained threshold but not high enough for Fit, the status is set to Strained.
    /// - If any attribute is below the Strained threshold, the status is set to Exhausted.
    /// The method retrieves all dwarves from the database, evaluates their attributes,
    /// and assigns the corresponding status.
    /// </remarks>
    private void StatusControl()
    {
        var dwarves = _context.Dwarves.ToList();
        
        foreach (var dwarf in dwarves)
        {
            dwarf.Status = GetStatus(dwarf);
        }
    }

    /// <summary>
    /// Determines the status of a single dwarf based on current energy, hunger, and thirst values.
    /// </summary>
    /// <param name="dwarf">The dwarf whose status should be evaluated.</param>
    /// <returns>
    /// Fit if all attributes are at or above the Fit threshold,
    /// Strained if all attributes are at or above the Strained threshold,
    /// otherwise Exhausted.
    /// </returns>
    private DwarfStatus GetStatus(Dwarf dwarf)
    {
        if (dwarf.Energy >= FitThreshold && dwarf.Hunger >= FitThreshold && dwarf.Thirst >= FitThreshold)
        {
            return DwarfStatus.Fit;
        }

        if (dwarf.Energy >= StrainThreshold && dwarf.Hunger >= StrainThreshold && dwarf.Thirst >= StrainThreshold)
        {
            return DwarfStatus.Strained;
        }

        return DwarfStatus.Exhausted;
    }
}