using DwarfColony.Data;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.Entities.World;
using Microsoft.EntityFrameworkCore;

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
        var dwarves = _context.
            Dwarves.
            Include(d => d.TargetArea).
            Where(d => d.State == DwarfState.Traveling && d.TargetAreaId != null && d.TravelRemainingTicks > 0).
            ToList();

        foreach (var dwarf in dwarves)
        {
            MoveToArea(dwarf, dwarf.TargetArea);
            if (dwarf.TravelRemainingTicks > 0)
            {
                dwarf.TravelRemainingTicks--;
            }
            else
            {
                dwarf.CurrentArea = dwarf.TargetArea;
                dwarf.State = DwarfState.Idle;
                dwarf.TargetAreaId = null;
            }
            
        }
    }
}