using DwarfColony.Data;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.Entities.World;

namespace DwarfColony.Services;

public class DwarfMoveToArea(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    private void MoveToArea(Dwarf dwarf, Area area)
    {
        if (dwarf.CurrentAreaId == area.Id)
            return;

        if (dwarf.State == DwarfState.Traveling)
            return;

        dwarf.State = DwarfState.Traveling;
        dwarf.TargetAreaId = area.Id;
        dwarf.TravelRemainingTicks = area.DistanceFromBase;
    }

    public void HandleTravelTick()
    {
        // Pridej podminku pokud spi atd.. 
        var dwarf = _context.
            Dwarves.
            Where(d => d.State == DwarfState.Traveling && d.TargetAreaId != null && d.TravelRemainingTicks > 0).
            ToList();
        
        
    }
}