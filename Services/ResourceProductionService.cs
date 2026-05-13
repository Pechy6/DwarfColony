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

    public void ProduceManager()
    {
        Produce();
    }

    /// <summary>
    /// Simuluje produkci zdrojů všemi trpaslíky v kolonii na základě jejich přidělené práce.
    /// Aktualizuje storage o nově vyprodukované zdroje, jako je jídlo, kámen, železo nebo dřevo.
    /// - Kuchaři produkují jídlo z neopracovaných surovin (raw food)
    /// - Horníci produkují kámen, uhlí nebo železnou rudu, která je určena náhodně.
    /// - Dřevorubci produkují dřevo.
    /// Metoda načte trpaslíky a storage z databáze, vypočítá produkci
    /// podle práce jednotlivých trpaslíků a aktualizuje odpovídající množství zdrojů ve storage.
    /// Změny jsou následně uloženy do databáze pomocí SaveChanges().
    /// Vyhodí výjimku, pokud v databázi neexistuje žádný storage.
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
                storage.Food += producedMaterial;
                storage.RawFood -= producedMaterial;
            }

            else if (dwarf.Job == DwarfJob.Woodcutter && IsDwarfInProduceLocation(dwarf))
            {
                storage.Wood += _resourceAreaService.ResourceTakenFromArea(dwarf, _wood, producedMaterial);
            }

            else if (dwarf.Job == DwarfJob.Miner && IsDwarfInProduceLocation(dwarf))
            {
                storage.Stone += _resourceAreaService.ResourceTakenFromArea(dwarf, _stone, producedMaterial);
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
                (r.ResourceType.Name == _stone && r.Amount > 0) ||
                (r.ResourceType.Name == _iron && r.Amount > 0) ||
                (r.ResourceType.Name == _coal && r.Amount > 0));
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