using DwarfColony.Data;
using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class ResourceProductionService(ApplicationDbContext context)
{
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
            var producedMaterial = ProductionResourceByStatus(dwarf);

            if (dwarf.Job == DwarfJob.Cook)
            {
                storage.Food += producedMaterial;
            }

            else if (dwarf.Job == DwarfJob.Miner)
            {
                var resources = GetRandomResource();

                if (resources == 0)
                {
                    storage.Stone += producedMaterial;
                }
                else if (resources == 1)
                {
                    storage.IronCore += producedMaterial;
                }
                else
                {
                    storage.Coal += producedMaterial;
                }
            }

            else if (dwarf.Job == DwarfJob.Woodcutter)
            {
                storage.Wood += producedMaterial;
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

    /// <summary>
    /// Vrací náhodnou hodnotu 0 nebo 1.
    /// Používá se pro určení, zda trpaslík ve špatném stavu vyprodukuje alespoň 1 jednotku surovin, nebo nic.
    /// </summary>
    /// <returns>0 nebo 1.</returns>
    private int GetRandomChance()
    {
        return _random.Next(0, 2);
    }

    private bool IsDwarfSleeping(Dwarf dwarf)
    {
        return dwarf.State == DwarfState.Sleeping;
    }

    /// <summary>
    /// Určuje množství surovin vyprodukovaných trpaslíkem podle jeho aktuálního stavu.
    /// Trpaslík ve stavu Fit vyprodukuje 2 jednotky.
    /// Trpaslík ve stavu Strained vyprodukuje 1 jednotku.
    /// Trpaslík ve zhoršeném stavu má náhodnou šanci vyprodukovat 1 jednotku, nebo nic.
    /// </summary>
    /// <param name="dwarf">Trpaslík, pro kterého se počítá produkce surovin. Jeho stav určuje výsledné množství.</param>
    /// <returns>Počet jednotek surovin vyprodukovaných trpaslíkem. Hodnota může být 0, 1 nebo 2 podle stavu a náhody.</returns>
    private int ProductionResourceByStatus(Dwarf dwarf)
    {
        if (dwarf.Status == DwarfStatus.Fit)
            return 2;
        if (dwarf.Status == DwarfStatus.Strained)
            return 1;
        return GetRandomChance() == 0
            ? 1
            : 0;
    }
}