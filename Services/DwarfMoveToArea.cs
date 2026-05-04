using DwarfColony.Data;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.Entities.World;
using Microsoft.EntityFrameworkCore;

namespace DwarfColony.Services;

public class DwarfMoveToArea(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public void StartTravel(Dwarf dwarf, Area area)
    {
        if (dwarf.CurrentAreaId == area.Id)
            return;

        if (dwarf.State == DwarfState.Traveling)
            return;

        dwarf.State = DwarfState.Traveling;
        dwarf.TargetAreaId = area.Id;
        dwarf.TravelRemainingTicks = area.DistanceFromBase;
    }

    private void TravelLogic()
    {
        var dwarves = _context.
            Dwarves.
            Include(d => d.TargetArea).
            Where(d => d.State == DwarfState.Traveling && d.TargetAreaId != null && d.TravelRemainingTicks > 0).
            ToList();

        foreach (var dwarf in dwarves)
        {
            if (dwarf.TargetArea == null)
                return;
            
            dwarf.TravelRemainingTicks--;

            if (dwarf.TravelRemainingTicks <= 0)
            {
                dwarf.CurrentArea = dwarf.TargetArea;
                dwarf.TargetAreaId = null;
                dwarf.State = DwarfState.Idle;
                dwarf.TravelRemainingTicks = 0;
            }
        }
    }

    public void HandleTravelTick()
    {
        TravelLogic();
    }
}