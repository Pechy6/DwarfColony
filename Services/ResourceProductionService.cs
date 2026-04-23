using DwarfColony.Data;
using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class ResourceProductionService(ApplicationDbContext context)
{
    // Database context

    // Resources produced by dwarves
    private readonly int _rawFood = 1;
    private readonly int _wather = 1;
    private readonly int _stone = 1;
    private readonly int _ironCore = 1;
    private readonly int _coal = 1;
    private readonly int _wood = 1;

    // Random number generator
    private readonly Random _random = new Random();

    public void ProduceManager()
    {
        Produce();
    }

    /// <summary>
    /// Simulates resource production by all dwarves in the colony based on their assigned jobs.
    /// Updates the storage with produced resources such as food, stone, iron, or wood.
    /// - Cooks produce food.
    /// - Miners produce either stone, coal or iron ore, determined randomly.
    /// - Woodcutters produce wood.
    /// The method retrieves dwarves and storage data from the database, calculates production
    /// based on each dwarf's job, and updates the corresponding resource counts in the storage.
    /// Changes are persisted to the database using SaveChanges().
    /// Throws an exception if no storage is found.
    /// </summary>
    /// <exception cref="Exception">
    /// Thrown when there is no storage entity available in the database.
    /// </exception>
    private void Produce()
    {
        var dwarves = context.Dwarves.ToList();
        var storage = context.Storages.FirstOrDefault() ?? throw new Exception("No storage found");

        foreach (var dwarf in dwarves)
        {
            if (IsDwarfSleeping(dwarf))
                continue;

            if (dwarf.Job == DwarfJob.Cook)
            {
                storage.Food += _rawFood;
            }

            else if (dwarf.Job == DwarfJob.Miner)
            {
                var resources = GetRandomResource();
                
                if (resources == 0)
                {
                    storage.Stone += _stone;
                }
                else if (resources == 1)
                {
                    storage.IronCore += _ironCore;
                }
                else
                {
                    storage.Coal += _coal;
                }
            }

            else if (dwarf.Job == DwarfJob.Woodcutter)
            {
                storage.Wood += _wood;
            }
        }
    }

    /// <summary>
    /// Returns a random integer between 0 and 2, representing a resource type.
    /// </summary>
    /// <returns></returns>
    private int GetRandomResource()
    {
        return _random.Next(0, 3);
    }

    private bool IsDwarfSleeping(Dwarf dwarf)
    {
        return dwarf.State == DwarfState.Sleeping;
    }
}