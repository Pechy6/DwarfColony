using DwarfColony.Data;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.Entities.World;

namespace DwarfColony.Services;

public class ResourceAreaService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    /// <summary>
    /// Odebere z oblasti, ve které se trpaslík právě nachází, požadované množství zadaného typu suroviny.
    /// </summary>
    /// <param name="dwarf">Trpaslík, který se pokouší odebrat suroviny z oblasti.</param>
    /// <param name="resourceType">Typ suroviny, která se má odebrat.</param>
    /// <param name="amount">Maximální množství suroviny, které se má odebrat.</param>
    /// <returns>
    /// Skutečně odebrané množství suroviny z oblasti.
    /// Vrací 0, pokud trpaslík není v žádné oblasti,
    /// požadovaný typ suroviny v oblasti není dostupný,
    /// nebo je zadané množství neplatné.
    /// </returns>
    public int ResourceTakenFromArea(Dwarf dwarf, string resourceType, int amount)
    {
        if (dwarf.CurrentArea is null || amount <= 0)
        {
            return 0;
        }

        var areaResource = dwarf.CurrentArea.Resources.FirstOrDefault(ar => ar.ResourceType.Name == resourceType);
        if (areaResource is null || areaResource.Amount <= 0)
        {
            return 0;
        }

        var realTaken = Math.Min(amount, areaResource.Amount);
        areaResource.Amount -= realTaken;
        return realTaken;
    }
}