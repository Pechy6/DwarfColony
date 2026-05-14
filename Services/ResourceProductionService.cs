using DwarfColony.Data;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.Entities.World;
using DwarfStatus = DwarfColony.Models.Entities.Dwarfs.DwarfStatus;
using Microsoft.EntityFrameworkCore;

namespace DwarfColony.Services;

public class ResourceProductionService(ApplicationDbContext context)
{
    private readonly ResourceAreaService _resourceAreaService = new ResourceAreaService(context);

    // Random number generator
    private readonly Random _random = new Random();

    private const string _wood = "Wood";
    private const string _stone = "Stone";
    private const string _iron = "Iron ore";
    private const string _coal = "Coal";
    private const string _gold = "Gold ore";

    public void ProduceManager()
    {
        Produce();
    }

    /// <summary>
    /// Simuluje produkci zdrojů všemi aktivními trpaslíky v kolonii na základě jejich přidělené práce.
    /// Spící trpaslíci jsou při produkci přeskočeni.
    /// - Kuchaři zpracovávají RawFood na Food podle dostupného množství surovin.
    /// - Dřevorubci produkují Wood z aktuální lokace, pokud se nachází v produkční oblasti.
    /// - Horníci vždy těží Stone z aktuální lokace a zároveň mají šanci vytěžit jeden vzácný zdroj.
    /// Vzácné zdroje jsou načteny podle aktuální lokace trpaslíka.
    /// Pokud má vzácný zdroj dostupné množství a náhodný hod projde přes jeho ChanceToMine,
    /// je do storage přičten odpovídající zdroj a množství dostupného vzácného zdroje v lokaci se sníží.
    /// V jednom produkčním cyklu může horník vytěžit maximálně jeden vzácný zdroj.
    /// Metoda načte trpaslíky, jejich aktuální lokace, dostupné zdroje a storage z databáze.
    /// Vypočítá produkci podle práce jednotlivých trpaslíků a změny následně uloží pomocí SaveChanges().
    /// </summary>
    /// <exception cref="Exception">
    /// Vyhozena v případě, že v databázi není dostupná žádná storage entita.
    /// </exception>
    private void Produce()
    {
        var dwarves = context.
            Dwarves.
            Include(d => d.CurrentArea).
            ThenInclude(a => a.Resources).
            ThenInclude(r => r.ResourceType).
            ToList();

        var areas = context.Areas.ToList();
        var storage = context.Storages.FirstOrDefault() ?? throw new Exception("No storage found");

        foreach (var dwarf in dwarves)
        {
            if (IsDwarfSleeping(dwarf))
                continue;
            var producedMaterial = ProductionResourceByStatus(dwarf);

            if (dwarf.Job == DwarfJob.Cook && storage.RawFood > 0)
            {
                var processedFood = Math.Min(storage.RawFood, producedMaterial);
                storage.Food += processedFood;
                storage.RawFood -= processedFood;
            }

            else if (dwarf.Job == DwarfJob.Woodcutter && IsDwarfInProduceLocation(dwarf))
            {
                storage.Wood += _resourceAreaService.ResourceTakenFromArea(dwarf, _wood, producedMaterial);
            }

            else if (dwarf.Job == DwarfJob.Miner && IsDwarfInProduceLocation(dwarf))
            {
                storage.Stone += _resourceAreaService.ResourceTakenFromArea(dwarf, _stone, producedMaterial);
                var rareResources = context.
                    RareResources.
                    Where(r => r.AreaId == dwarf.CurrentAreaId).
                    Include(r => r.ResourceType).
                    ToList();

                var availableRareResources = rareResources.
                    Where(r => r.Amount > 0).
                    OrderBy(_ => Guid.NewGuid()).
                    ToList();

                foreach (var resource in availableRareResources)
                {
                    double roll = Random.Shared.NextDouble();

                    if (roll <= resource.ChanceToMine)
                    {
                        switch (resource.ResourceType.Name)
                        {
                            case _iron:
                                storage.IronOre += 1;
                                resource.Amount -= 1;
                                break;
                            case _coal:
                                storage.Coal += 1;
                                resource.Amount -= 1;
                                break;
                            case _gold:
                                storage.GoldOre += 1;
                                resource.Amount -= 1;
                                break;
                        }

                        break;
                    }
                }
            }
        }

        context.SaveChanges();
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

    private bool IsDwarfInProduceLocation(Dwarf dwarf)
    {
        var currentArea = dwarf.CurrentArea;
        if (currentArea is null)
        {
            return false;
        }

        var currentJob = dwarf.Job;

        if (currentJob == DwarfJob.Woodcutter)
        {
            var resourceType = currentArea.Resources.Any(r => r.ResourceType.Name == _wood && r.Amount > 0);
            if (resourceType)
                return true;
        }

        if (currentJob == DwarfJob.Miner)
        {
            var resourceType = currentArea.Resources.Any(r =>
                (r.ResourceType.Name == _stone && r.Amount > 0));

            if (resourceType)
                return true;
        }

        return false;
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