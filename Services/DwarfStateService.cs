using DwarfColony.Data;
using DwarfColony.Models.Entities;

namespace DwarfColony.Services;

public class DwarfStateService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public void ChangeState(Dwarf dwarf)
    {
        if (dwarf.State != DwarfState.Sleeping)
        {
            if (dwarf.Job == DwarfJob.None)
            {
                dwarf.State = DwarfState.Idle;
            }
            else
            {
                dwarf.State = DwarfState.Working;
            }
        }
    }
}